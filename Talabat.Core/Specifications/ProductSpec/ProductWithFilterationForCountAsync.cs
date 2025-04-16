using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpec
{
    public class ProductWithFilterationForCountAsync:BaseSpecification<Product>
    {
        public ProductWithFilterationForCountAsync(ProductSpecParams specParams) :
            base(p =>
               (string.IsNullOrEmpty(specParams.Search)  || p.Name.ToLower().Contains(specParams.Search)&&

               (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId)
            &&
               (!specParams.TypeId.HasValue  || p.ProductTypeId==specParams.TypeId)))
        {

            
        }
    }
}
