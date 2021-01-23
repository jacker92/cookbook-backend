﻿using CookbookAPI.Models.Requests;
using CookbookAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookbookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;

        public LoginController(IUserService userService, ILogger<LoginController> logger)
        {
            _userService = userService;
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
    }
}
