using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CookbookAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoRepository<Recipe> _recipeRepository;
        private readonly IHttpContextAccessor _httpContext;
        public RecipeService(IMongoRepository<Recipe> recipeRepository, IHttpContextAccessor httpContext)
        {
            _recipeRepository = recipeRepository;
            _httpContext = httpContext;
        }

        public Recipe Get(string id)
        {
            return _recipeRepository.FindById(id);
        }

        public CreateNewRecipeResponse Create(CreateNewRecipeRequest createNewRecipeRequest)
        {
            ModelValidator.Validate(createNewRecipeRequest);
            var recipe = new Recipe()
            {
                ID = new ObjectId(),
                Name = createNewRecipeRequest.Name,
                Instructions = createNewRecipeRequest.Instructions,
                Ingredients = createNewRecipeRequest.Ingredients,
                URL = createNewRecipeRequest.URL,
                Author = _httpContext.HttpContext.Items["User"] as User
            };

            _recipeRepository.InsertOne(recipe);
            return new CreateNewRecipeResponse { Recipe = recipe };
        }

        public void Update(Recipe recipe)
        {
            _recipeRepository.ReplaceOne(recipe);
        }

        public void Remove(Recipe recipeIn)
        {
            _recipeRepository.DeleteOne(recipe => recipe.ID == recipeIn.ID);
        }

        public void Remove(string id)
        {
            _recipeRepository.DeleteOne(recipe => recipe.ID.ToString() == id);
        }

        public IList<Recipe> Get()
        {
            return _recipeRepository.AsQueryable().ToList();
        }
    }
}
