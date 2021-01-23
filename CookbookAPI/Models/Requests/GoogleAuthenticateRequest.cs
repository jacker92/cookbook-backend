using System.ComponentModel.DataAnnotations;

namespace CookbookAPI.Models.Requests
{
    public class GoogleAuthenticateRequest
    {
        [Required]
        public string GoogleToken { get; set; }
    }
}