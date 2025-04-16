using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryContract;
using Talabat.Core.ServiceContract;
using Talabat.Core.Specifications.ProductSpec;
using Talabat.Store.Dto;
using Talabat.Store.Errors;
using Talabat.Store.Helpers;

namespace Talabat.Store.Controllers
{
 
    public class ProductController :ApiBaseController
    {
        private readonly IProductServices _productServices;

        /// private readonly IGenericRepository<Product> _productRepo;
        /// private readonly IGenericRepository<ProductBrand> _brandRepo;
        /// private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;
                                //Ask Clr Creat Object and Allow depenecyinjection in program
        public ProductController(
                                 IProductServices productServices ,
                               /// IGenericRepository<Product> productRepo
                               ///,IGenericRepository<ProductBrand> brandRepo,
                               /// IGenericRepository<ProductType> typeRepo,
                                 IMapper mapper)
        {
            ///_productRepo = productRepo;
            ///_brandRepo = brandRepo;
            ///_typeRepo = typeRepo;
            _productServices = productServices;
            _mapper = mapper;
        }
       // [Authorize]
        [HttpGet]
        // getAllproduct
        public async Task<ActionResult<IReadOnlyList</*Product*/Pagination<ProductToReturnDto>>>> GetAllProduct([FromQuery]ProductSpecParams SpecParams)
        {
            // var product= await _productRepo.GetAll();
            var products = await _productServices.GetProductAsync(SpecParams);
            var Count = await _productServices.GetCountAsync(SpecParams);
            var MappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok( new Pagination<ProductToReturnDto>(SpecParams.PageSize,SpecParams.PageIndex, MappedProduct, Count));
        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult</*Product*/ProductToReturnDto>> GetById(int id)
        {
            var product = await _productServices.GetProductAsync(id);
            if (product is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
            
        }


        [HttpGet("brands")]

        public async Task< ActionResult< IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var brands= await _productServices.GetproductBrandAsync();
            return Ok(brands);
        }
        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetType()
        {
            var types= await _productServices.GetProductTypeAsync(); 
            return Ok(types);
        }



    }
}
