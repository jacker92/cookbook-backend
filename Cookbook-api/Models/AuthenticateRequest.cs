using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook_api.Models
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GoogleToken { get; set; }
    }
}
