﻿using CookbookAPI.Models;
using CookbookAPI.Repositories;
using CookbookAPI.Utilities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookbookAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _users;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<UserService> _logger;
        private readonly GoogleTokenValidator _googleTokenValidator;

        public UserService(IMongoRepository<User> users,
            JwtTokenGenerator jwtTokenGenerator,
            ILogger<UserService> logger,
            GoogleTokenValidator googleTokenValidator)
        {
            _users = users;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _googleTokenValidator = googleTokenValidator;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.FilterBy(x => x.UserName == model.Username &&
                                   x.Password == model.Password &&
                                   x.AccountType == AccountType.Internal).FirstOrDefault();

            if (user == null) return null;

            var token = _jwtTokenGenerator.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest request)
        {
            var result = _googleTokenValidator.ValidateGoogleToken(request)?.Result;

            if (result == null)
            {
                return null;
            }

            var user = _users.FilterBy(x => x.UserName == result.Email).FirstOrDefault();

            var userResult = user ?? CreateFromGoogleInfo(result);

            var token = _jwtTokenGenerator.GenerateJwtToken(userResult);

            return new AuthenticateResponse(userResult, token);
        }

        private User CreateFromGoogleInfo(GoogleAuthenticateResponse result)
        {
            var user = new User
            {
                AccountType = AccountType.Google,
                FirstName = result.Given_Name,
                LastName = result.Family_Name,
                UserName = result.Email
            };
            _users.InsertOne(user);
            return user;
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
            } catch (ValidationException)
            {
                return null;
            }

            var response = _users.FilterBy(x => x.UserName == request.UserName).FirstOrDefault();

            if(response != null)
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
