using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeleviryMethod deleviryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeleviryMethod = deleviryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail {  get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        public Address ShippingAddress {  get; set; }
        public DeleviryMethod DeleviryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set;    } = new HashSet<OrderItem>();
        public decimal SubTotal {  get; set; }
        public decimal GetTotal() => SubTotal+DeleviryMethod.Cost;
        public string PaymentIntentId { get; set; } = "";
        
    }
}
