using FlightAdvisor.Core.Helpers.Authorization;
using FlightAdvisor.Domain.Entities;
using FlightAdvisor.Interfaces.Repositories;
using FlightAdvisor.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FlightAdvisor.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;

        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            return _userRepository.GetWhere(predicate);
        }

        public void Add(User user)
        {
            user.Salt = HashPasswordHelper.GenerateSalt();
            user.Password = HashPasswordHelper.HashPassword(user.Password, user.Salt);
            _userRepository.Add(user);
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository
                .GetWhere(x => x.Username == username && x.Password == HashPasswordHelper.HashPassword(password, x.Salt)).FirstOrDefault();

            if (user == null)
                return null;

            return GenerateTokenForUser(user);          
        }

        private User GenerateTokenForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;
            user.Salt = null;

            return user;
        }
    }
}
