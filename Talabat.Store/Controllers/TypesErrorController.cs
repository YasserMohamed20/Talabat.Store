using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.Store.Controllers
{
    
    public class TypesErrorController : ApiBaseController
    {
        private readonly StoreDbContext _dbContext;

        public TypesErrorController(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("serverError")]
        public ActionResult ServerError()
        {
            var product = _dbContext.products.Find(100);

            return Ok(product.ToString());
        }
    }
}
