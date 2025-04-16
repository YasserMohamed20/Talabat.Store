using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata.Ecma335;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryContract;
using Talabat.Store.Dto;
using Talabat.Store.Errors;

namespace Talabat.Store.Controllers
{
   
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRep;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRep ,IMapper mapper)
        {
            _basketRep = basketRep;
            _mapper = mapper;
        }
        //GetBasket
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string BasketId)
        {
            var Basket= await _basketRep.GetBasketAsunc(BasketId);
            return  Basket is null ? new CustomerBasket(BasketId) : Ok(Basket);
        }


        //UpdateBasket
        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket( CustomerBasketDto CustomerBasket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(CustomerBasket);
            var CreateOrUpdate =await _basketRep.UpdateBasketAsync(MappedBasket);
            if (CreateOrUpdate is null) return BadRequest(new ApiResponse(400));
            else return Ok(CreateOrUpdate);


        }




        //DeleteBsket
        [HttpDelete]

        public async Task<bool> DeleteBasket(string id)
        {
            return await _basketRep.DeleteBasketAsync(id);
        }
    }
}
