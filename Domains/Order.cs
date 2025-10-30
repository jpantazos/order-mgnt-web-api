namespace CibusMed_App.Domains
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public OrderStatus Status { get; set; }
        public string? Notes { get; set; }

        public Customer Customer { get; set; } = null;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
