using CookbookAPI.Models.Requests;
using CookbookAPI.Services;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CookbookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login(AuthenticateRequest model)
        {
            _logger.LogInformation("In login", model);

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.GoogleToken != null)
            {
                return ProcessGoogleRequest(model);
            }

            return ProcessBasicRequest(model);
        }

        private IActionResult ProcessBasicRequest(AuthenticateRequest model)
        {
            var basicRequest = new BasicAuthenticateRequest
            {
                UserName = model.UserName,
                Password = model.Password
            };

            ModelValidator.Validate(basicRequest);

            var res = _loginService.Authenticate(basicRequest);

            if (res == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(res);
        }

        private IActionResult ProcessGoogleRequest(AuthenticateRequest model)
        {
            var googleRequest = new GoogleAuthenticateRequest
            {
                GoogleToken = model.GoogleToken
            };

            var response = _loginService.AuthenticateWithGoogle(googleRequest);
            if (response == null)
                return BadRequest(new { message = "Invalid token" });

            return Ok(response);
        }
    }
}
