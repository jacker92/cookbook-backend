using System.ComponentModel.DataAnnotations;

namespace CookbookAPI.Models.Requests
{
    public class BasicAuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
