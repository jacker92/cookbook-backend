using CookbookAPI.Models;
using System.Collections.Generic;

namespace CookbookAPI.Services
{
    public interface IRecipeService
    {
        Recipe Create(Recipe recipe);
        IList<Recipe> Get();
        Recipe Get(string id);
        void Remove(Recipe recipeIn);
        void Remove(string id);
        void Update(Recipe recipeIn);
    }
}