using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.Identity;
using Talabat.Core.RepositoryContract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Store.Errors;
using Talabat.Store.Extensions;
using Talabat.Store.Helpers;
using Talabat.Store.MiddelWars;

namespace Talabat.Store
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Configrations
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //?? ????? Swagger ???? ??? SchemaId 
            builder.Services.AddSwaggerGen(c => { c.CustomSchemaIds(type => type.FullName); });

            builder.Services.AddDbContext<StoreDbContext>
                (opetion=>opetion.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));
            //Extension

            #region Dependency Injection to connect to redis

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>

            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);

            }
            );

            //Allow Dependency injection To Class Immplement interface IBasketRepository
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            #endregion

            #region Allwo Dependency Injection to ConnectionString
            builder.Services.AddDbContext<AppIdentityDbContext>(option =>

            option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))

            );
            #endregion


            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServces(builder.Configuration);

            #endregion

            var app = builder.Build();

            // create Scop

           using var Scop=app.Services.CreateAsyncScope();
            var services=Scop.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreDbContext>();
            var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var UserManager=services.GetRequiredService<UserManager<AppUser>>();
            var logerfactor =services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await SeedDbContext.AsyncSeed(_dbContext);
                await _IdentityDbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.IdentitySeedAsync(UserManager);
            }
            catch (Exception ex)
            {
                var loger=logerfactor.CreateLogger<Program>();
                loger.LogError(ex, "An Error Occurs During Applay to Database");
               
            }

            // Configure the HTTP request pipeline.
            // app.UseMiddleware<ExceptionMiddelWare>();
            app.UseDeveloperExceptionPage();
            #region Configer Http Request  pipeline

            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddelWare>();
                app.UseSwaggerMiddelWare();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();

            app.UseHttpsRedirection();
          
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();


            #endregion
            app.Run();
        }
    }
}
