using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.ServiceContract;
using Talabat.Core.Specifications;
using Talabat.Store.Dto;
using Talabat.Store.Errors;

namespace Talabat.Store.Controllers
{
    [Authorize]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            // هنا باخد الاميل من التوكن بدل من ببعته مباشر في ال اندبوينت
            var buyerEmial = User.FindFirstValue(ClaimTypes.Email);
           var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(buyerEmial, orderDto.BasketId, orderDto.deliveryMethodId,address);
            if (order is null)
                return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));

        }
        //
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            // هنا باخد الاميل من التوكن بدل من ببعته مباشر في ال اندبوينت
            var buyerEmail =User.FindFirstValue(ClaimTypes.Email);
            var orders=await _orderService.GetOrderForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOerderForUser(int id)
        {
            // هنا باخد الاميل من التوكن بدل من ببعته مباشر في ال اندبوينت
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderByIdForUser(id, buyerEmail);
            if (orders is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(orders));

        }

        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeleviryMethod>>> GetDeliveryMethod()
        {
            var dliveryMethod = await _orderService.GetDeleviryMethodAsync();
            return Ok(dliveryMethod);
        }
    }
}
