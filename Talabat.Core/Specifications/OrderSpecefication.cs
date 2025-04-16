using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class OrderSpecefication :BaseSpecification<Order>
    {
        public OrderSpecefication(string byerEmail):base(O=>O.BuyerEmail== byerEmail)
        {
            Includes.Add(O => O.DeleviryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDecs(O => O.OrderDate);
        }

        public OrderSpecefication(int orderId,string byerEmail)
            :base(O=>O.BuyerEmail==byerEmail&& O.Id==orderId)
        {
            Includes.Add(O => O.DeleviryMethod);
            Includes.Add(O => O.Items);
        }

       
    }
}
