using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using CookbookAPI.Controllers.WebApi.Controllers;
using CookbookAPI.Services;
using CookbookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CookbookAPI.Tests.TestData;
using Newtonsoft.Json;
using CookbookAPI.Utilities;

namespace CookbookAPI.Tests.Controllers
{
    [TestClass()]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        private Mock<IUserService> _usersService;
        private Mock<ILogger<UsersController>> _logger;

        [TestInitialize]
        public void InitTests()
        {
            _usersService = new Mock<IUserService>();
            _logger = new Mock<ILogger<UsersController>>();
            _usersController = new UsersController(_usersService.Object, _logger.Object);
        }
        [TestMethod()]
        public void Create_ShouldWorkAndCallUserService_IfCalledWithCorrectModel()
        {
            var request = TestDataRepository.GetCreateNewUserRequest();
            var response = TestDataRepository.GetCreateNewUserResponse();

            _usersService.Setup(x => x.Create(request))
                .Returns(response);

            var result = (OkObjectResult)_usersController.Create(request);

            var res = result.Value as CreateNewUserResponse;

            Assert.AreEqual(request.FirstName, res.User.FirstName);
            Assert.AreEqual(request.LastName, res.User.LastName);
            Assert.AreEqual(request.UserName, res.User.UserName);
            Assert.IsTrue(SecurePasswordHasher.Verify(request.Password, res.User.Password));
        }
    }
}