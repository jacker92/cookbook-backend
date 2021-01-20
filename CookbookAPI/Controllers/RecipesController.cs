using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CookbookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _recipeService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetRecipe")]
        public ActionResult<Recipe> Get(string id)
        {
            var recipe = _recipeService.Get(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        [HttpPost]
        public ActionResult<CreateNewRecipeResponse> Create(CreateNewRecipeRequest request)
        {
            try
            {
                var result = _recipeService.Create(request);
                return CreatedAtAction("GetRecipe", result);
            }
            catch
            {
                return BadRequest(new { message = "Could not create recipe" });
            }
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(Recipe recipe)
        {
            var result = _recipeService.Get(recipe.ID.ToString());

            if (result == null)
            {
                return NotFound();
            }

            _recipeService.Update(result);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var recipe = _recipeService.Get(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _recipeService.Remove(recipe);

            return NoContent();
        }
    }
}
