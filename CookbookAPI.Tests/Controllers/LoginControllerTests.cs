using CookbookAPI.Controllers;
using CookbookAPI.Models;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Services;
using CookbookAPI.Tests.TestData;
using CookbookAPI.Tests.Utilities;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CookbookAPI.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        private LoginController _loginController;
        private Mock<IMongoRepository<User>> _usersRepository;
        private ILoginService _userService;
        private ILogger<LoginController> _logger;

        [TestInitialize]
        public void InitTests()
        {
            _logger = new Mock<ILogger<LoginController>>().Object;
            _usersRepository = new Mock<IMongoRepository<User>>();
            _userService = new LoginService(new JwtTokenGenerator(TestHelper.CreateTestAppSettings()),
                                            new GoogleTokenValidator(),
                                            _usersRepository.Object);
            _loginController = new LoginController(_userService, _logger);
        }

        [TestMethod()]
        public void Login_WhenCalledWithInvalidGoogleToken_ShouldReturnBadResult()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();

            var result = (BadRequestObjectResult)_loginController.Login(request);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Login_WhenCalledWithNoMatchDatabase_ShouldReturnBadResult()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();
            request.GoogleToken = null;

            var result = (BadRequestObjectResult)_loginController.Login(request);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Login_WhenCalledWithCorrectUserNameAndPassword_ShouldReturnOk()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();
            request.GoogleToken = null;

            var user = TestDataRepository.BuildUser();

            _usersRepository.Setup(x => x.FilterBy(It.IsAny<Expression<Func<User,bool>>>()))
                .Returns(new List<User> { user });

            var result = (OkObjectResult)_loginController.Login(request);

            Assert.IsNotNull(result);

            var res = result.Value as AuthenticateResponse;

            Assert.IsNotNull(res);
            Assert.AreEqual(request.UserName, res.Username);
            Assert.AreEqual(user.FirstName, res.FirstName);
            Assert.AreEqual(user.LastName, res.LastName);
            Assert.AreEqual(user.UserName, res.Username);
        }

        [TestMethod()]
        [ExpectedException(typeof(ValidationException))]
        public void Login_WhenCalledWithPasswordMissing_ShouldReturnValidationError()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();
            request.GoogleToken = null;
            request.Password = null;

            var user = TestDataRepository.BuildUser();

            _usersRepository.Setup(x => x.FilterBy(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User> { user });

            var result = (OkObjectResult)_loginController.Login(request);

            Assert.Fail("Expected validationexception to occur");
        }

        [TestMethod()]
        public void Login_WhenCalledWithIncorrectPassword_ShouldReturnNotAuthorized()
        {
            var request = TestDataRepository.BuildAuthenticateRequest();
            request.GoogleToken = null;

            var user = TestDataRepository.BuildUser();
            user.Password = "asdf".Hash();

            _usersRepository.Setup(x => x.FilterBy(x => x.UserName.Equals(request.UserName) &&
                                    x.AccountType == AccountType.Internal)
                                    ).Returns(new List<User> { user });

            var result = (BadRequestObjectResult)_loginController.Login(request);

            Assert.IsNotNull(result);
        }
    }
}
