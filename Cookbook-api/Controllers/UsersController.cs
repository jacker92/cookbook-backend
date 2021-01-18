using Cookbook_api.Models;
using Cookbook_api.Services;
using Cookbook_api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook_api.Controllers
{
    namespace WebApi.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly IUserService _userService;
            private readonly ILogger<UsersController> _logger;

            public UsersController(IUserService userService, ILogger<UsersController> logger)
            {
                _userService = userService;
                _logger = logger;
            }

            [HttpPost("authenticate")]
            public IActionResult Authenticate(AuthenticateRequest model)
            {
                _logger.LogInformation("In authenticate");
                if (model is null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                if (model.GoogleToken != null)
                {
                    var response = _userService.AuthenticateWithGoogle(model);
                    if (response == null)
                        return BadRequest(new { message = "Invalid token" });

                    return Ok(response);
                }

                var res = _userService.Authenticate(model);

                if (res == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(res);
            }

            [Authorize]
            [HttpGet]
            public IActionResult GetAll()
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
        }
    }
}
