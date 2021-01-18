using Cookbook_api.Models;
using Cookbook_api.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cookbook_api.Utilities
{
    public class GoogleTokenValidator
    {
        private readonly string GOOGLE_TOKEN_API_BASE = @"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";
        public async Task<GoogleAuthenticateResponse> ValidateGoogleToken(AuthenticateRequest model)
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
