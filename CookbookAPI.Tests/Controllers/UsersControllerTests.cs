using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using CookbookAPI.Controllers.WebApi.Controllers;
using CookbookAPI.Services;
using CookbookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CookbookAPI.Tests.TestData;
using CookbookAPI.Utilities;
using CookbookAPI.Repositories;
using CookbookAPI.Models.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using CookbookAPI.Tests.Utilities;

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
            _usersService = new UserService(_usersRepository.Object, 
                                            new JwtTokenGenerator(TestHelper.CreateTestAppSettings()), 
                                            new GoogleTokenValidator());
            _usersController = new UsersController(_usersService, _logger.Object);
        }
        [TestMethod()]
        public void Create_ShouldReturnOK_IfCalledWithCorrectModel()
        {
            var request = TestDataRepository.BuildCreateNewUserRequest();

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
            var request = TestDataRepository.BuildCreateNewUserRequest();
            request.Password = null;

            var result = (BadRequestObjectResult)_usersController.Create(request);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Authenticate_WhenCalledWithInvalidGoogleToken_ShouldReturnBadResult()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();

            var result = (BadRequestObjectResult)_usersController.Authenticate(request);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Authenticate_WhenCalledWithInvalidSetup_ShouldReturnBadResult()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();
            request.GoogleToken = null;

            var result = (BadRequestObjectResult)_usersController.Authenticate(request);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Authenticate_WhenCalledWithCorrectUserNameAndPassword_ShouldReturnOk()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();
            request.GoogleToken = null;

            var user = TestDataRepository.BuildUser();

            _usersRepository.Setup(x => x.FilterBy(x => x.UserName.Equals(request.Username) &&
                                    x.AccountType == AccountType.Internal &&
                                    SecurePasswordHasher.Verify(request.Password, x.Password))
                                    ).Returns(new List<User> { user });

            var result = (OkObjectResult)_usersController.Authenticate(request);

            Assert.IsNotNull(result);
        }
    }
}