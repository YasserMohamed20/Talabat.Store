using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Talabat.Store.Errors;

namespace Talabat.Store.MiddelWars
{
    public class ExceptionMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddelWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddelWare(RequestDelegate next,ILogger<ExceptionMiddelWare> logger,IHostEnvironment env)
        {
           _next = next;
           _logger = logger;
           _env = env;
        }
        // method is call invok 

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch ( Exception ex)
            {

                _logger.LogError(ex,ex.Message);// During Development

                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                //Convert PascalCase to Camlcase
                var options=new JsonSerializerOptions()
                { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                };

                var json=JsonSerializer.Serialize(response, options);
               
               await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
