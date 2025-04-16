using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.RepositoryContract;
using Talabat.Core.ServiceContract;
using Talabat.Repository;
using Talabat.Services;
using Talabat.Store.Errors;
using Talabat.Store.Helpers;

namespace Talabat.Store.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IProductServices), typeof(ProductServices));
           // Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            // two method are correct 
            // builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfilies()));
           Services.AddAutoMapper(typeof(MappingProfilies));

            #region ValidationErrror
            // Import ValidationErrror 

            Services.Configure<ApiBehaviorOptions>(options =>
              options.InvalidModelStateResponseFactory = (actionContext) =>
              {
                  var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                     .SelectMany(p => p.Value.Errors)
                                                     .Select(p => p.ErrorMessage).ToList();
                  var response = new ApiValidationErrorResponse
                  {
                      Error = errors
                  };

                  return new BadRequestObjectResult(response);
              }

              ); 
            #endregion


            return Services;
        }
    }
}
