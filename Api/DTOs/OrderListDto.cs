using CibusMed_App.Domains;

namespace CibusMed_App.Api.DTOs
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductNames { get; set; } = string.Empty;
    }
}
