using Cookbook_api.Models;
using System.Collections.Generic;

namespace Cookbook_api.Services
{
    public interface IRecipeService
    {
        Recipe Create(Recipe recipe);
        IList<Recipe> Get();
        Recipe Get(string id);
        void Remove(Recipe recipeIn);
        void Remove(string id);
        void Update(string id, Recipe recipeIn);
    }
}