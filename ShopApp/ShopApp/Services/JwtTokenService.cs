using Google.Apis.Auth;
using ShopApp.Models;

namespace ShopApp.Services
{
    public interface IJwtTokenService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogelToken(ExternalLoginRequest request);
    }
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogelToken(ExternalLoginRequest request)
        {
            //_configuration["Authentication:Google:ClientId"];
            string clientId = _configuration["Authentication:Google:ClientId"];//"977621133056-f3vvvb7evmme0348afesskmcf37h2srv.apps.googleusercontent.com";
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { clientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);
            return payload;
        }
    }
}
