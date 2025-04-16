using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Store.Dto
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int deliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
    }
}
