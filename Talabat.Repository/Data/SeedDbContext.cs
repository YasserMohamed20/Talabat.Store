using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class SeedDbContext
    {
        public async static Task AsyncSeed(StoreDbContext _dbContext)
        {
            

            #region Barand
            if (!_dbContext.productBrands.Any())
            {

                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    foreach (var brand in Brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion

            #region type
            if (!_dbContext.productTypes.Any())
            {

                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        _dbContext.Set<ProductType>().Add(type);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion

            #region product
            if (!_dbContext.products.Any())
            {

                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion

            if (!_dbContext.deleviryMethods.Any())
            {
                var DeliveryMethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var DeliveryMethod = JsonSerializer.Deserialize<List<DeleviryMethod>>(DeliveryMethodData);
                if (DeliveryMethod?.Count>0)
                {
                    foreach (var deliveryMethod in DeliveryMethod)
                    {
                        _dbContext.Set<DeleviryMethod>().Add(deliveryMethod);

                    }
                    await _dbContext.SaveChangesAsync();
                } 
            }
        }
    }
}
