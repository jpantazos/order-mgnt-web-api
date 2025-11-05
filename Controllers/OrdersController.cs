using CibusMed_App.Api.Data;
using CibusMed_App.Api.DTOs;
using CibusMed_App.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CibusMed_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _context;

    public OrdersController(OrderDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get list of orders with optional filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderListDto>>> GetOrders(
        [FromQuery] OrderStatus? status = null,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        [FromQuery] string? q = null)
    {
        var query = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsQueryable();

        // Apply filtering
        if (status.HasValue)
        {
            query = query.Where(o => o.Status == status.Value);
        }

        if (from.HasValue)
        {
            query = query.Where(o => o.CreatedAtUtc >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(o => o.CreatedAtUtc <= to.Value);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            var searchItem = q.ToLower();

            query = query.Where(o => o.Customer.Name.ToLower().Contains(searchItem));
        }

        var orders = await query
            .OrderByDescending(o => o.CreatedAtUtc)
            .Select(o => new OrderListDto
            {
                Id = o.Id,
                CustomerName = o.Customer.Name,
                CreatedAtUtc = o.CreatedAtUtc,
                Status = o.Status,
                TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice),
                ProductNames = string.Join(", ", o.OrderItems.Select(oi => oi.Product.Name))
            })
            .ToListAsync();

        return Ok(orders);
    }

    /// <summary>
    /// Get order by unique identifier ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailsDto>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound($"Order with id {id} not found!");
        }

        var orderDto = new OrderDetailsDto
        {
            Id = order.Id,
            CreatedAtUtc = order.CreatedAtUtc,
            Status = order.Status,
            Notes = order.Notes,
            Customer = new CustomerDto
            {
                Id = order.Customer.Id,
                Name = order.Customer.Name,
                Email = order.Customer.Email
            },
            OrderItems = order.OrderItems.Select(oi =>
                new OrderItemDto
                {
                    Id = oi.Id,
                    ProductName = oi.Product.Name,
                    ProductSku = oi.Product.Sku,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    LineTotal = oi.LineTotal,
                }).ToList()
        };

        return Ok(orderDto);
    }

    /// <summary>
    /// Update the order status
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto dto)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound($"Order with id {id} not found.");
        }

        // validation of status
        if (!IsValidStatus(order.Status, dto.NewStatus))
        {
            return BadRequest($"Cannot change status from {order.Status} to {dto.NewStatus}." +
                GetValidMsg(order.Status));
        }

        order.Status = dto.NewStatus;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Validates if a status is allowed
    /// </summary>
    public static bool IsValidStatus(OrderStatus currentStatus, OrderStatus newStatus)
    {
        return currentStatus switch
        {
            OrderStatus.Pending => newStatus == OrderStatus.Approved || newStatus == OrderStatus.Cancelled,
            OrderStatus.Approved => newStatus == OrderStatus.Shipped || newStatus == OrderStatus.Cancelled,
            OrderStatus.Shipped => false, // Terminal status
            OrderStatus.Cancelled => false, // Terminal status
            _ => false
        };
    }

    private static string GetValidMsg(OrderStatus currentStatus)
    {
        return currentStatus switch
        {
            OrderStatus.Pending => "Valid transitions: Approved, Cancelled",
            OrderStatus.Approved => "Valid transitions: Shipped, Cancelled",
            OrderStatus.Shipped => "Shipped is a terminal status. No further changes allowed.",
            OrderStatus.Cancelled => "Cancelled is a terminal status. No further changes allowed.",
            _ => "Unknown status"
        };
    }
}
