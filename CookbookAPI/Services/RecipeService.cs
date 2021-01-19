using CookbookAPI.Models;
using CookbookAPI.Repositories;
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

        public Recipe Get(string id) =>
           _recipeRepository.FindById(id);

        public Recipe Create(Recipe recipe)
        {
            _recipeRepository.InsertOne(recipe);
            return recipe;
        }

        public void Update(Recipe recipe) =>
            _recipeRepository.ReplaceOne(recipe);

        public void Remove(Recipe recipeIn) =>
            _recipeRepository.DeleteOne(recipe => recipe.ID == recipeIn.ID);

        public void Remove(string id) =>
            _recipeRepository.DeleteOne(recipe => recipe.ID.ToString() == id);

        public IList<Recipe> Get() => _recipeRepository.AsQueryable().ToList();
    }
}
