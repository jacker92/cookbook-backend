using CookbookAPI.Models.Requests;
using CookbookAPI.Services;
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
                var response = _loginService.AuthenticateWithGoogle(model);
                if (response == null)
                    return BadRequest(new { message = "Invalid token" });

                return Ok(response);
            }

            var res = _loginService.Authenticate(model);

            if (res == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(res);
        }
    }
}
