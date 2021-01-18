using Cookbook_api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook_api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMongoCollection<Recipe> _recipes;

        public RecipeService(ICookbookDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _recipes = database.GetCollection<Recipe>(settings.RecipesCollectionName);
        }

        public Recipe Get(string id) =>
           _recipes.Find(recipe => recipe.ID == id).FirstOrDefault();

        public Recipe Create(Recipe recipe)
        {
            _recipes.InsertOne(recipe);
            return recipe;
        }

        public void Update(string id, Recipe recipeIn) =>
            _recipes.ReplaceOne(recipe => recipe.ID == id, recipeIn);

        public void Remove(Recipe recipeIn) =>
            _recipes.DeleteOne(recipe => recipe.ID == recipeIn.ID);

        public void Remove(string id) =>
            _recipes.DeleteOne(recipe => recipe.ID == id);

        public IList<Recipe> Get() => _recipes.Find(recipe => true).ToList();
    }
}
