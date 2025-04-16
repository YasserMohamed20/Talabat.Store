using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Store.Dto
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }
        public string DeleviryMethod { get; set; }
        public decimal DeleviryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal Total {  get; set; }
        public string PaymentIntentId { get; set; } = "";

    }
}
