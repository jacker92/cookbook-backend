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
using CookbookAPI.Repositories;

namespace CookbookAPI.Tests.Controllers
{
    [TestClass()]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        private UserService _usersService;
        private Mock<ILogger<UsersController>> _logger;
        private Mock<IMongoRepository<User>> _usersRepository;

        [TestInitialize]
        public void InitTests()
        {
            _usersRepository = new Mock<IMongoRepository<User>>();
            _logger = new Mock<ILogger<UsersController>>();
            _usersService = new UserService(_usersRepository.Object, null, null, null);
            _usersController = new UsersController(_usersService, _logger.Object);
        }
        [TestMethod()]
        public void Create_ShouldReturnOK_IfCalledWithCorrectModel()
        {
            var request = TestDataRepository.GetCreateNewUserRequest();

            var result = (OkObjectResult)_usersController.Create(request);

            var res = result.Value as CreateNewUserResponse;

            Assert.AreEqual(request.FirstName, res.User.FirstName);
            Assert.AreEqual(request.LastName, res.User.LastName);
            Assert.AreEqual(request.UserName, res.User.UserName);
            Assert.IsTrue(SecurePasswordHasher.Verify(request.Password, res.User.Password));
        }

        [TestMethod()]
        public void Create_ShouldReturnBadResult_IfCalledWithInvalidModel()
        {
            var request = TestDataRepository.GetCreateNewUserRequest();
            request.Password = null;

            var result = (BadRequestObjectResult)_usersController.Create(request);

            Assert.IsNotNull(result);
        }
    }
}