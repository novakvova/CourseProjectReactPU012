using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginRequest request)
        {
            var data = await _jwtTokenService.VerifyGoogelToken(request);
            return Ok();
        }
    }
}
