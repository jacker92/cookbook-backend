using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Utilities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CookbookAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _users;

        public UserService(IMongoRepository<User> users)
        {
            _users = users;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.AsQueryable().ToList();
        }

        public User GetById(string id)
        {
            return _users.FindById(id);
        }

        public CreateNewUserResponse Create(CreateNewUserRequest request)
        {
            try
            {
                ValidateRequest(request);
            }
            catch (ValidationException)
            {
                return null;
            }

            var response = _users.FilterBy(x => x.UserName == request.UserName).FirstOrDefault();

            if (response != null)
            {
                throw new Exception($"User with username: {request.UserName} already exists.");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                AccountType = AccountType.Internal,
                Password = request.Password.Hash(),
            };

            _users.InsertOne(user);
            return new CreateNewUserResponse { User = user };
        }

        private void ValidateRequest(CreateNewUserRequest request)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(request, new ValidationContext(request), validationResults))
            {
                throw new ValidationException(nameof(request));
            }
        }
    }
}
