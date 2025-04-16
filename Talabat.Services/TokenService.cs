using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.ServiceContract;

namespace Talabat.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager)
        {
            //payload
            //1- private claim [User - defined]
            var AuthClaim = new List<Claim>()
            {
               new Claim(ClaimTypes.GivenName, user.Name),
               new Claim(ClaimTypes.Email, user.Email)

            };
            // Add Roles of User is in UserManager
            var UserRoles = await userManager.GetRolesAsync(user);
            foreach(var Role in UserRoles)
            {
                AuthClaim.Add(new Claim(ClaimTypes.Role, Role));
            }

            // Add Key                                                   use _configuration as Dictionary
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["JWT:Key"]));
            //use objet to create token
            var Token = new JwtSecurityToken(
                //Add Register To Claim
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClaim,
                //Add Key and Algorithm 
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)

                );
            //generate token
            return new JwtSecurityTokenHandler().WriteToken(Token);
              
        }
    }
}
