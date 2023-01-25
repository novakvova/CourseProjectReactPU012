using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Data.Entities.Identity;
using ShopApp.Models;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(IJwtTokenService jwtTokenService,
            UserManager<AppUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { error = "Дані вказано не вірно" });
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                return BadRequest(new { error = "Дані вказано не вірно" });
            }
            string token = await _jwtTokenService.CreateToken(user);
            return Ok(new { token });
        }


        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest request)
        {
            var data = await _jwtTokenService.VerifyGoogelToken(request);
            if(data== null)
            {
                return BadRequest(new { error = "Не віркний токен" });
            }
            return Ok();
        }
    }
}
