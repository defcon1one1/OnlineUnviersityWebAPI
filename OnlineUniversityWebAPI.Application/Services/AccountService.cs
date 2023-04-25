using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineUniversityWebAPI.Application.Authentication;
using OnlineUniversityWebAPI.Application.Exceptions;
using OnlineUniversityWebAPI.Application.Services.Interfaces;
using OnlineUniversityWebAPI.Domain.Entities;
using OnlineUniversityWebAPI.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OnlineUniversityWebAPI.Application.Models.Dtos;

namespace OnlineUniversityWebAPI.Application.Services
{

    public class AccountService : IAccountService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly OnlineUniversityWebAPIDbContext _dbContext;
        public AccountService(IPasswordHasher<User> passwordHasher, OnlineUniversityWebAPIDbContext dbContext, AuthenticationSettings authenticationSettings) 
        { 
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterStudent(RegisterStudentDto dto)
        {
            var studentRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "Student");
            var newStudent = new Student()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                RoleId = studentRole.Id
            };


            var hashedPassword = _passwordHasher.HashPassword(newStudent, dto.Password);
            newStudent.PasswordHash = hashedPassword;
            _dbContext.Add(newStudent);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }


            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
