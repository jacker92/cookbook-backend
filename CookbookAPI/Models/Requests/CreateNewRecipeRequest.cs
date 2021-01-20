using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
