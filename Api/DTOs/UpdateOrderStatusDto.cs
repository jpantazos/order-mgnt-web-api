using CibusMed_App.Domains;

namespace CibusMed_App.Api.DTOs
{
    public class UpdateOrderStatusDto
    {
        public OrderStatus NewStatus { get; set; }
    }
}
