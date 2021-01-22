using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookbookAPI.Models.Requests
{
    public class CreateNewRecipeRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public IList<RecipeIncredient> Ingredients { get; set; }

        public string URL { get; set; }
    }
}
