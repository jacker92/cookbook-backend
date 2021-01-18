﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook_api.Models
{

    public interface ICookbookDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string RecipesCollectionName { get; set; }
        string UsersCollectionName { get; set; }
    }
}
