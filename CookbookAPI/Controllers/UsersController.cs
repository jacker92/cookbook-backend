using CookbookAPI.Models.Requests;
using CookbookAPI.Services;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CookbookAPI.Controllers
{
    namespace WebApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly IUserService _userService;

            public UsersController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpPost]
            public IActionResult Create(CreateNewUserRequest request)
            {
                var res = _userService.Create(request);

                if (res == null)
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
