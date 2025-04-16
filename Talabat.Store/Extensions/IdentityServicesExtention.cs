using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.ServiceContract;
using Talabat.Repository.Data.Configrations;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace Talabat.Store.Extensions
{
    public static class IdentityServicesExtention
    {                                                   // means that is caller no paramter
        public static IServiceCollection AddIdentityServces(this IServiceCollection Services,IConfiguration configuration)
        {

            Services.AddScoped<ITokenService, TokenService>();
            Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();


            Services.AddAuthentication(option =>
            {   //Default Scheme is Bearer
                option.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).
                AddJwtBearer(option=>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // validate on Toke With
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromDays(Double.Parse(configuration["JWT:DurationInDays"]))


                    };


                    
                });
            return Services;
        }
    }
}
