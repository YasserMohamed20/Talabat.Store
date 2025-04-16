using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.ServiceContract
{
    public interface IOrderService
    {
        // CreateOrder
         Task<Order?> CreateOrderAsync(string buyerEmial, string basketId, int deliveryMethod, Address shipingAddress);
         Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
         Task<Order?> GetOrderByIdForUser(int orderId,string buyerEmail);
        Task<IReadOnlyList< DeleviryMethod>> GetDeleviryMethodAsync();

    }
}
