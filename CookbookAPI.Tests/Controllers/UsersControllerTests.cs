using CookbookAPI.Controllers.WebApi.Controllers;
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
using System.Collections.Generic;

namespace CookbookAPI.Tests.Controllers
{
    [TestClass()]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        private UserService _usersService;
        private Mock<IMongoRepository<User>> _usersRepository;

        [TestInitialize]
        public void InitTests()
        {
            _usersRepository = new Mock<IMongoRepository<User>>();
            _usersService = new UserService(_usersRepository.Object,
                                            new JwtTokenGenerator(TestHelper.CreateTestAppSettings()),
                                            new GoogleTokenValidator());
            _usersController = new UsersController(_usersService);
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

        [TestMethod]
        public void GetAll_ShouldReturnOK()
        {
            var result = (OkObjectResult)_usersController.GetAll();

            Assert.IsNotNull(result);
        }
    }
}