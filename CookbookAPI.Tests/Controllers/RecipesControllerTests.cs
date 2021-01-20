using CookbookAPI.Controllers;
using CookbookAPI.Models;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Services;
using CookbookAPI.Tests.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookbookAPI.Tests.Controllers
{
    [TestClass]
    public class RecipesControllerTests
    {
        private RecipesController _recipesController;
        private RecipeService _recipeService;
        private Mock<IMongoRepository<Recipe>> _repository;

        [TestInitialize]
        public void InitTests()
        {
            _repository = new Mock<IMongoRepository<Recipe>>();
            _recipeService = new RecipeService(_repository.Object);
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
    }
}
