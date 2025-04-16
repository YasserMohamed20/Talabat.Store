using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.ServiceContract;
using Talabat.Store.Dto;
using Talabat.Store.Errors;
using Talabat.Store.Extensions;

namespace Talabat.Store.Controllers
{
   
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser>_userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;

        public AccountsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService ,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "An Email is use"));
            }
            var User = new AppUser()
            {
                Name = model.Name,
                Email = model.Email,
                UserName=model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            
            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded)
                return BadRequest(new ApiResponse(400));
            var UserReturned = new UserDto()
            {
                Email = User.Email,
                Name = User.Name,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)

            };
            return Ok(UserReturned);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto model)
        {
            var User=await _userManager.FindByEmailAsync(model.Email);
            if(User is null) return Unauthorized(new ApiResponse(401));

            var Result=await _signInManager.CheckPasswordSignInAsync(User, model.Password,false);
            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));

            var UserReturned = new UserDto()
            {
                Name = User.Name,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };
            return Ok(UserReturned);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var Email= User.FindFirstValue(ClaimTypes.Email);
            var user=await _userManager.FindByEmailAsync(Email);
            var ReturnedObject = new UserDto()
            {
                Email = user.Email,
                Name = user.Name,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturnedObject);

        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<Address>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindUserAddressWithAsync(User);
            var AddresMapped = mapper.Map<Address, AddressDto>(user.Address);
            return Ok(AddresMapped);

        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult< AddressDto>> UpdateAddress(AddressDto addressUpdated)
        {
            var user= await _userManager.FindUserAddressWithAsync(User);
            var AddressMapped =  mapper.Map< AddressDto,Address>(addressUpdated);
            AddressMapped.Id = user.Address.Id;
            user.Address = AddressMapped;
            var Result =await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(AddressMapped);
        }
        [Authorize]
        [HttpGet("emailExists")]
        public async Task<ActionResult< bool>> CheckEmailExists(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
