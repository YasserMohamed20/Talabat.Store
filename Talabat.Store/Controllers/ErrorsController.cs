using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Store.Errors;

namespace Talabat.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi =true)]
        public ActionResult Errors(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
