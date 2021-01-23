using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookbookAPI.Utilities
{
    public class GoogleTokenValidator
    {
        private readonly string GOOGLE_TOKEN_API_BASE = @"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";
        public async Task<GoogleAuthenticateResponse> ValidateGoogleToken(GoogleAuthenticateRequest model)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{GOOGLE_TOKEN_API_BASE}{model.GoogleToken}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GoogleAuthenticateResponse>(content);
                }
            }
            return null;
        }
    }
}
