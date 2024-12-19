using E_commerce.Interface;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;


namespace E_commerce.Services
{
    public class UserService : IUserService
    {
        private IDatabaseService<User> _databaseService;
        private IConfiguration _configuration;
        public UserService(IDatabaseService<User> databaseService, IConfiguration configuration)
        {
            _databaseService = databaseService;
            _databaseService.SetCollection(nameof(User));
            _configuration = configuration;
        }

        public async Task<List<User>> GetAsync()
        {
            return await _databaseService.GetAllAsync();
        }

        public async Task RegisterUserAsync(User user)
        {
            await _databaseService.AddAsync(user);
        }

        public async Task UnRegisterUserAsync(User user)
        {
            await _databaseService.DeleteAsync(user.Id);
        }

        public async Task<string> GetTokenAsync(string userName, string password)
        {
            var user = await _databaseService.FindAsync(userName, password);

            if (user == null)
            {
                return null;
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
             new Claim(JwtRegisteredClaimNames.Name, userName),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token ID
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            var token1 = new JwtSecurityTokenHandler().WriteToken(token);
            return (token1);

        }
    }
}