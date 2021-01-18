using CookbookAPI.Models;
using CookbookAPI.Services;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CookbookAPI.Controllers
{
    namespace WebApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
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
                _logger.LogInformation("In authenticate", model);

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

            [HttpPost]
            public IActionResult Create(CreateNewUserRequest request)
            {
                var res = _userService.Create(request);

                if(res == null)
                    return BadRequest(new { message = "Could not create user" });

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
