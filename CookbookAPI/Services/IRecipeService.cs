using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using MongoDB.Bson;
using System.Collections.Generic;

namespace CookbookAPI.Services
{
    public interface IRecipeService
    {
        CreateNewRecipeResponse Create(CreateNewRecipeRequest recipe);
        IList<Recipe> Get();
        Recipe Get(string id);
        void Remove(Recipe recipeIn);
        void Remove(string id);
        void Update(Recipe recipeIn);
    }
}