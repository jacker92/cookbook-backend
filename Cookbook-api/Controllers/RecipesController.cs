using Cookbook_api.Models;
using Cookbook_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IRecipeService _recipeService;

        public RecipesController(ILogger<RecipesController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }


        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            _logger.LogInformation("Getting all recipes");
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
        public ActionResult<Recipe> Create(Recipe recipe)
        {
            _recipeService.Create(recipe);

            return CreatedAtRoute("GetRecipe", new { id = recipe.ID.ToString() }, recipe);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Recipe recipeIn)
        {
            var book = _recipeService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _recipeService.Update(id, recipeIn);

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

            _recipeService.Remove(recipe.ID);

            return NoContent();
        }
    }
}
