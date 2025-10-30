using CibusMed_App.Domains;
using Microsoft.EntityFrameworkCore;

namespace CibusMed_App.Api.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

        // Configure decimal precision
        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2);

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Customers
        var customers = new[]
        {
            new Customer { Id = 1, Name = "John Smith", Email = "john.smith@email.com" },
            new Customer { Id = 2, Name = "Sarah Johnson", Email = "sarah.johnson@email.com" },
            new Customer { Id = 3, Name = "Michael Brown", Email = "michael.brown@email.com" },
            new Customer { Id = 4, Name = "Emily Davis", Email = "emily.davis@email.com" },
            new Customer { Id = 5, Name = "David Wilson", Email = "david.wilson@email.com" },
            new Customer { Id = 6, Name = "Lisa Anderson", Email = "lisa.anderson@email.com" },
            new Customer { Id = 7, Name = "James Taylor", Email = "james.taylor@email.com" },
            new Customer { Id = 8, Name = "Maria Garcia", Email = "maria.garcia@email.com" },
            new Customer { Id = 9, Name = "Robert Martinez", Email = "robert.martinez@email.com" },
            new Customer { Id = 10, Name = "Jennifer Lee", Email = "jennifer.lee@email.com" }
        };
        modelBuilder.Entity<Customer>().HasData(customers);

        // Seed Products
        var products = new[]
        {
            new Product { Id = 1, Sku = "PRD-001", Name = "Wireless Mouse", UnitPrice = 29.99m },
            new Product { Id = 2, Sku = "PRD-002", Name = "Mechanical Keyboard", UnitPrice = 89.99m },
            new Product { Id = 3, Sku = "PRD-003", Name = "USB-C Cable", UnitPrice = 12.99m },
            new Product { Id = 4, Sku = "PRD-004", Name = "Monitor Stand", UnitPrice = 45.99m },
            new Product { Id = 5, Sku = "PRD-005", Name = "Webcam HD", UnitPrice = 59.99m },
            new Product { Id = 6, Sku = "PRD-006", Name = "Headset", UnitPrice = 79.99m },
            new Product { Id = 7, Sku = "PRD-007", Name = "Laptop Sleeve", UnitPrice = 24.99m },
            new Product { Id = 8, Sku = "PRD-008", Name = "Desk Lamp", UnitPrice = 34.99m },
            new Product { Id = 9, Sku = "PRD-009", Name = "External SSD 1TB", UnitPrice = 129.99m },
            new Product { Id = 10, Sku = "PRD-010", Name = "Ergonomic Chair Cushion", UnitPrice = 39.99m }
        };
        modelBuilder.Entity<Product>().HasData(products);

        // Seed Orders (50 orders with at least one per customer and one in each status)
        var orders = new List<Order>();
        var random = new Random(42); // Fixed seed for consistency
        var statuses = new[] { OrderStatus.Pending, OrderStatus.Approved, OrderStatus.Shipped, OrderStatus.Cancelled };

        int orderId = 1;

        // Ensure at least one order in each status
        for (int i = 0; i < 4; i++)
        {
            orders.Add(new Order
            {
                Id = orderId++,
                CustomerId = (i % 10) + 1,
                CreatedAtUtc = DateTime.UtcNow.AddDays(-random.Next(1, 90)),
                Status = statuses[i],
                Notes = $"Test order in {statuses[i]} status"
            });
        }

        // Ensure at least one order per customer
        for (int customerId = 1; customerId <= 10; customerId++)
        {
            if (!orders.Any(o => o.CustomerId == customerId))
            {
                orders.Add(new Order
                {
                    Id = orderId++,
                    CustomerId = customerId,
                    CreatedAtUtc = DateTime.UtcNow.AddDays(-random.Next(1, 90)),
                    Status = statuses[random.Next(statuses.Length)],
                    Notes = $"Order for customer {customerId}"
                });
            }
        }

        // Fill remaining orders up to 50
        while (orders.Count < 50)
        {
            orders.Add(new Order
            {
                Id = orderId++,
                CustomerId = random.Next(1, 11),
                CreatedAtUtc = DateTime.UtcNow.AddDays(-random.Next(1, 90)),
                Status = statuses[random.Next(statuses.Length)],
                Notes = random.Next(2) == 0 ? $"Order #{orderId - 1}" : null
            });
        }

        modelBuilder.Entity<Order>().HasData(orders);

        // Seed OrderItems
        var orderItems = new List<OrderItem>();
        int itemId = 1;

        foreach (var order in orders)
        {
            int itemCount = random.Next(1, 5); // 1-4 items per order
            var usedProducts = new HashSet<int>();

            for (int i = 0; i < itemCount; i++)
            {
                int productId;
                do
                {
                    productId = random.Next(1, 11);
                } while (usedProducts.Contains(productId));

                usedProducts.Add(productId);

                var product = products.First(p => p.Id == productId);

                orderItems.Add(new OrderItem
                {
                    Id = itemId++,
                    OrderId = order.Id,
                    ProductId = productId,
                    Quantity = random.Next(1, 6),
                    UnitPrice = product.UnitPrice
                });
            }
        }

        modelBuilder.Entity<OrderItem>().HasData(orderItems);
    }
}