using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.ServiceContract;
using Talabat.Core.Specifications.ProductSpec;

namespace Talabat.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> GetProductAsync(ProductSpecParams specParams)
        {
            var Spec = new ProductwithBrandAndTypesSpecification(specParams);
            var product =await _unitOfWork.Repository<Product>().GetAllWithSpec(Spec);
            return product;
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            var Spec = new ProductwithBrandAndTypesSpecification(productId);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpec(Spec);
            return product;
        }

        public Task<IReadOnlyList<ProductBrand>> GetproductBrandAsync()
            => _unitOfWork.Repository<ProductBrand>().GetAll();
       
        public Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
            =>_unitOfWork.Repository<ProductType>().GetAll();
    
        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var Spec=new ProductWithFilterationForCountAsync(specParams);
            var Count=await _unitOfWork.Repository<Product>().GetCountWithSpec(Spec);
            return Count;
        }
        
    }
}
