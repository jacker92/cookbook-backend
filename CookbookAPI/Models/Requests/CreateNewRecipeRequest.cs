using System.ComponentModel.DataAnnotations;

namespace CookbookAPI.Models.Requests
{
    public class CreateNewRecipeRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
