﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectManagment.Api;
using ProjectManagment.Api.Models;
using ProjectManagment.Models;
using ProjectManagment.Models.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ProjectManagment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "ApiKeyPolicy")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ProjectManagmentContext _context;

        public LoginController(IConfiguration config, ProjectManagmentContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginDataModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                if (user.active)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    response = Ok(new { token = tokenString, user = user });
                }
                else
                {
                    response = Unauthorized("NOT_ACTIVATED");
                }
            }
            else
            {
                response = Unauthorized("BAD_CREDENTIALS");
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, userInfo.id.ToString())
             };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(LoginDataModel login)
        {
            string password = PasswordUtilities.HashPassword(login.password);

            User user = _context.User.Where(x => x.email == login.email && x.password == password).SingleOrDefault();

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}

