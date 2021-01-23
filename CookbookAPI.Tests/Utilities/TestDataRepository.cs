using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Utilities;
using MongoDB.Bson;
using System.Collections.Generic;

namespace CookbookAPI.Tests.TestData
{
    public static class TestDataRepository
    {
        public static CreateNewUserRequest BuildCreateNewUserRequest()
        {
            return new CreateNewUserRequest
            {
                UserName = "jaakko",
                FirstName = "jaakko",
                LastName = "lahtinen",
                Password = "P@ssw0rd"
            };
        }

        public static CreateNewUserResponse BuildCreateNewUserResponse()
        {
            return new CreateNewUserResponse
            {
                User = new User
                {
                    ID = new ObjectId(),
                    UserName = "jaakko",
                    FirstName = "jaakko",
                    LastName = "lahtinen",
                    Password = "P@ssw0rd".Hash()
                }
            };
        }

        public static AuthenticateRequest BuildAuthenticateRequest()
        {
            return new AuthenticateRequest
            {
                UserName = "test",
                Password = "P@ssw0rd",
                GoogleToken = "test"
            };
        }

        public static User BuildUser()
        {
            return new User
            {
                ID = new ObjectId(),
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                AccountType = AccountType.Internal,
                Password = "P@ssw0rd".Hash()
            };
        }

        public static Recipe BuildRecipe()
        {
            return new Recipe
            {
                ID = new ObjectId(),
                Name = "My Recipe",
                Instructions = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Vivamus malesuada tortor lacus, non vehicula eros maximus a. Suspendisse v" +
                "elit nibh, rhoncus id ligula sit amet, posuere malesuada nibh. Proin lobortis" +
                ", lacus sed pulvinar varius, nisl justo congue ipsum, vel consequat lacus leo " +
                "sit amet elit. Suspendisse lacinia purus vitae turpis aliquam, in cursus diam " +
                "vestibulum. Vivamus quis mauris at nisl eleifend malesuada et eget mauris. Nullam" +
                " augue nibh, maximus sit amet mattis sit amet, fermentum vitae augue. Aenean maxim" +
                "us sit amet urna eu dapibus. Vestibulum tristique felis eu aliquet facilisis. In" +
                "teger bibendum lacus eu interdum tincidunt."
            };
        }

        public static CreateNewRecipeRequest BuildCreateNewRecipeRequest()
        {
            return new CreateNewRecipeRequest
            {
                Name = "My Recipe",
                Instructions = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Vivamus malesuada tortor lacus, non vehicula eros maximus a. Suspendisse v" +
                "elit nibh, rhoncus id ligula sit amet, posuere malesuada nibh. Proin lobortis" +
                ", lacus sed pulvinar varius, nisl justo congue ipsum, vel consequat lacus leo " +
                "sit amet elit. Suspendisse lacinia purus vitae turpis aliquam, in cursus diam " +
                "vestibulum. Vivamus quis mauris at nisl eleifend malesuada et eget mauris. Nullam" +
                " augue nibh, maximus sit amet mattis sit amet, fermentum vitae augue. Aenean maxim" +
                "us sit amet urna eu dapibus. Vestibulum tristique felis eu aliquet facilisis. In" +
                "teger bibendum lacus eu interdum tincidunt.",
                Ingredients = new List<RecipeIncredient>
                {
                    new RecipeIncredient
                    {
                        Ingredient = new Incredient
                        {
                            Name = "Potato"
                        },
                        Amount = 1.5,
                        UnitOfMeasurement = UnitOfMeasurement.kg
                    }
                },
                URL = "www.google.fi"
            };
        }

        public static CreateNewRecipeResponse BuildCreateNewRecipeResponse()
        {
            return new CreateNewRecipeResponse
            {
                Recipe = new Recipe
                {
                    ID = new ObjectId(),
                    Name = "My Recipe",
                    Instructions = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Vivamus malesuada tortor lacus, non vehicula eros maximus a. Suspendisse v" +
                "elit nibh, rhoncus id ligula sit amet, posuere malesuada nibh. Proin lobortis" +
                ", lacus sed pulvinar varius, nisl justo congue ipsum, vel consequat lacus leo " +
                "sit amet elit. Suspendisse lacinia purus vitae turpis aliquam, in cursus diam " +
                "vestibulum. Vivamus quis mauris at nisl eleifend malesuada et eget mauris. Nullam" +
                " augue nibh, maximus sit amet mattis sit amet, fermentum vitae augue. Aenean maxim" +
                "us sit amet urna eu dapibus. Vestibulum tristique felis eu aliquet facilisis. In" +
                "teger bibendum lacus eu interdum tincidunt."
                }

            };
        }
    }
}
