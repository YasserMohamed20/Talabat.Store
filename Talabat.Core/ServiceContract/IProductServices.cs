using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.ProductSpec;

namespace Talabat.Core.ServiceContract
{
    public interface IProductServices
    {
        Task<IReadOnlyList<Product>> GetProductAsync(ProductSpecParams specParams);
        Task<Product?> GetProductAsync(int productId);
        Task<IReadOnlyList<ProductBrand>> GetproductBrandAsync();
        Task<IReadOnlyList< ProductType>> GetProductTypeAsync();
        Task<int> GetCountAsync(ProductSpecParams specParams);
    }
}
