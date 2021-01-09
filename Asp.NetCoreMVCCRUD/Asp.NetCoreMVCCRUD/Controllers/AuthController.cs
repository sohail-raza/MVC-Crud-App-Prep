using Asp.NetCoreMVCCRUD.Factories;
using Asp.NetCoreMVCCRUD.Models.Authentication;
using Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Controllers
{
    public class AuthController : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IAuthenticationService _authService;
        

        public AuthController(IJwtFactory jwtFactory, IAuthenticationService authService)
        {
            _jwtFactory = jwtFactory;
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsViewModel credentials)
        {

            // get user identity with credentials
            var userIdentity = await _authService.GetUserByEmailAsync(credentials.Email);
            if (userIdentity == null)
            {
                return Unauthorized();
            }

            var signinResult = await _authService.SigninAsync(userIdentity, credentials.Password);
            if (signinResult.Result == false)
            {
                return Unauthorized(signinResult);
            }

            var identity = await _jwtFactory.GetClaimsIdentityForUser(userIdentity);
            if (identity == null)
            {
                return Unauthorized();
            }

            var jwt = await _jwtFactory.GenerateJwt(identity, _jwtFactory, credentials.Email, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
    }


}
