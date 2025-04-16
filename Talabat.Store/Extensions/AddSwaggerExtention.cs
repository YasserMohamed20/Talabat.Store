using System.Runtime.CompilerServices;

namespace Talabat.Store.Extensions
{
    public static class AddSwaggerExtention
    {
        public static WebApplication UseSwaggerMiddelWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
