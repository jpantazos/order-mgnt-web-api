using CibusMed_App.Domains;

namespace CibusMed_App.Api.DTOs
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public OrderStatus Status { get; set; }
        public string? Notes { get; set; }
        public CustomerDto Customer { get; set; } = null!;
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public decimal TotalAmount => OrderItems.Sum(i => i.LineTotal);
    }
}
