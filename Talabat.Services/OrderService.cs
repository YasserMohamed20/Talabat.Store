using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.RepositoryContract;
using Talabat.Core.ServiceContract;
using Talabat.Core.Specifications;

namespace Talabat.Services
{
   
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
      // private readonly IGenericRepository<Product> _productRepo;
      // private readonly IGenericRepository<DeleviryMethod> _deliveryMethodRepo;
      // private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(
            IBasketRepository basketRepo,
            IUnitOfWork unitOfWork
         ///IGenericRepository<Product> ProductRepo,
         ///IGenericRepository<DeleviryMethod> deliveryMethodRepo
         ///,IGenericRepository<Order> OrderRepo
         )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        // _productRepo = ProductRepo;
        // _deliveryMethodRepo = deliveryMethodRepo;
        // _orderRepo = OrderRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmial, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1.Get Basket From Basket Repo
            var Basket = await _basketRepo.GetBasketAsunc(basketId);
             //2.Get Selected Items at Basket From Product Repo
             var orderItems= new List<OrderItem>();
            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                    var ProducItemOrder = new ProductItemOrder(item.Id,Product.Name,Product.PictureUrl);
                    var orderItem = new OrderItem(ProducItemOrder,Product.Price,item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
             //3.Calculate SubTotal
             var subTotal=orderItems.Sum(orderItem=>orderItem.Price*orderItem.Quantity);
            //4.Get Delivery Method From DeliveryMethod Repo
            var deliveryMethod = await _unitOfWork.Repository<DeleviryMethod>().GetById(deliveryMethodId);
             //5.Create Order
             var order=new Order(buyerEmial, shippingAddress, deliveryMethod, orderItems, subTotal);
             //6.Add Order Locally
             await _unitOfWork.Repository<Order>().AddAsync(order);
             //7.Save Order To Database[ToDo]
             var result= await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;  
             
        }

     

        public async Task<Order?> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            var Spec = new OrderSpecefication(orderId, buyerEmail);

            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpec(Spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {

            var spec=new OrderSpecefication(buyerEmail);
            var order=await _unitOfWork.Repository<Order>().GetAllWithSpec(spec);
            return order;

        }
        public async Task<IReadOnlyList< DeleviryMethod>> GetDeleviryMethodAsync()
            =>await _unitOfWork.Repository<DeleviryMethod>().GetAll();
    
    }
}
