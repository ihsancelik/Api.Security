using Api.Security.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Security.Controllers
{
    [ApiController, Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost, Route("authenticate")]
        public TokenInfo Authenticate(AuthenticateModel model)
        {
            var tokenInfo = authenticationService.Authenticate(model.Username, model.Password, model.ClientType);
            return tokenInfo;
        }
    }

    public class AuthenticateModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientType { get; set; }
    }
}
