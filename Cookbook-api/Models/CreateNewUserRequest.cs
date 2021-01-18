using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook_api.Models
{
    public class CreateNewUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
