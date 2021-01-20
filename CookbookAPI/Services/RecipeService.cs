using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CookbookAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoRepository<Recipe> _recipeRepository;

        public RecipeService(IMongoRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
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
                Instructions = createNewRecipeRequest.Instructions
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
