﻿using CookbookAPI.Controllers;
using CookbookAPI.Models;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Services;
using CookbookAPI.Tests.TestData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using System.Linq;

namespace CookbookAPI.Tests.Controllers
{
    [TestClass]
    public class RecipesControllerTests
    {
        private RecipesController _recipesController;
        private RecipeService _recipeService;
        private Mock<IMongoRepository<Recipe>> _repository;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private User _user;

        [TestInitialize]
        public void InitTests()
        {
            _repository = new Mock<IMongoRepository<Recipe>>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _user = TestDataRepository.BuildUser();
            _httpContextAccessor.Setup(x => x.HttpContext.Items["User"]).Returns(_user);
            _recipeService = new RecipeService(_repository.Object, _httpContextAccessor.Object);
            _recipesController = new RecipesController(_recipeService);
        }

        [TestMethod]
        public void Get_ShouldReturnOk_AndEmptyIEnumerable()
        {
            var result = _recipesController.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetByID_ShouldReturnOk_AndValidResult_IfObjectIsFound()
        {
            var recipe = TestDataRepository.BuildRecipe();
            _repository.Setup(x => x.FindById(recipe.ID.ToString())).Returns(recipe);
            var result = _recipesController.Get(recipe.ID.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(recipe, result.Value);
        }

        [TestMethod]
        public void GetByID_ShouldReturnNotFound_IfObjectIsNotFound()
        {
            var result = _recipesController.Get(new ObjectId().ToString());

            var res = result.Result as NotFoundResult;

            Assert.IsNotNull(res);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Create_ShouldReturn201_WhenSucceeded()
        {
            var recipeRequest = TestDataRepository.BuildCreateNewRecipeRequest();

            var result = _recipesController.Create(recipeRequest);

            var res = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(res);

            var value = (CreateNewRecipeResponse)res.Value;
            Assert.IsNotNull(value);

            Assert.AreEqual(value.Recipe.Name, recipeRequest.Name);
            Assert.AreEqual(value.Recipe.Instructions, recipeRequest.Instructions);
            Assert.AreEqual(value.Recipe.Ingredients, recipeRequest.Ingredients);
            Assert.AreEqual(value.Recipe.URL, recipeRequest.URL);
            Assert.AreEqual(value.Recipe.Author, _user);
        }

        [TestMethod]
        public void Created_ShouldReturnBadResult_WhenPostedInvalidModel()
        {
            var recipe = TestDataRepository.BuildCreateNewRecipeRequest();
            recipe.Name = null;
            recipe.Instructions = null;

            var result = _recipesController.Create(recipe);

            var res = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Update_ShouldReturnNoContent_WhenUpdatedExistingRecipe()
        {
            var recipe = TestDataRepository.BuildRecipe();
            _repository.Setup(x => x.FindById(recipe.ID.ToString())).Returns(recipe);
            var result = _recipesController.Update(recipe);

            var res = result as NoContentResult;
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Update_ShouldReturnNotFound_WhenUpdatedNonExistingRecipe()
        {
            var recipe = TestDataRepository.BuildRecipe();
            var result = _recipesController.Update(recipe);

            var res = result as NotFoundResult;
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Delete_ShouldReturnNoContent_WhenDeletingExistingRecipe()
        {
            var recipe = TestDataRepository.BuildRecipe();
            _repository.Setup(x => x.FindById(recipe.ID.ToString())).Returns(recipe);
            var result = _recipesController.Delete(recipe.ID.ToString());

            var res = result as NoContentResult;
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound_WhenDeletingNonExistingRecipe()
        {
            var recipe = TestDataRepository.BuildRecipe();
            var result = _recipesController.Delete(recipe.ID.ToString());

            var res = result as NotFoundResult;
            Assert.IsNotNull(res);
        }
    }
}
