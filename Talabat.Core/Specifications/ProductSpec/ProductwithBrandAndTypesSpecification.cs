using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpec
{
    public class ProductwithBrandAndTypesSpecification:BaseSpecification<Product>
    {
        // this ctor will be used for creating an object ,that will be used to get all product 
        public ProductwithBrandAndTypesSpecification(ProductSpecParams SpecParams) :
            base(p=>
                      (string.IsNullOrEmpty(SpecParams.Search) || p.Name.ToLower().Contains(SpecParams.Search)) &&
                      (!SpecParams.BrandId.HasValue || p.ProductBrandId != SpecParams.BrandId.Value) &&

                      (!SpecParams.TypeId.HasValue  || p.ProductTypeId==SpecParams.TypeId.Value)
            )
        {
            ApplayIncludes();

            if (!string.IsNullOrEmpty(SpecParams.Sort))
            {
                switch (SpecParams.Sort)
                {
                    case "priceAsc":
                        OrderBy = p => p.Price;
                        break;
                    case "priceDesc":
                        OrderByDesc = p => p.Price;
                        break;
                    default:
                        OrderBy = p => p.Name;
                        break;
                }
            }
            else
            {
                OrderBy = p => p.Name;
            }
            // pagesiz=5
            //page index=3
            // skip frist 10 
            ApplayPagenation((SpecParams.PageIndex - 1) * SpecParams.PageSize, SpecParams.PageSize);
        }
        //this ctor is will be used Creating an object , that will be used get with specifict product with Id
        public ProductwithBrandAndTypesSpecification(int id) : base(p => p.Id == id)
        {
            ApplayIncludes();
        }

        private void ApplayIncludes()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

        }
    }
}
