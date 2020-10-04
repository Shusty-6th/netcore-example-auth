using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCoreExampleAuth.Entities.Models;
using NetCoreExampleAuth.Patterns.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreExampleAuth.BusinessLogic
{
    // TODO: configuration => IOptions
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private User user;
        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationContract userForAuth)
        {
            this.user = await this.userManager.FindByNameAsync(userForAuth.UserName);

            return (this.user != null && await this.userManager.CheckPasswordAsync(this.user, userForAuth.Password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <returns>Returns secret key as a byte array with the security algorithm.</returns>
        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = configuration.GetSection("JwtSettings").GetSection("secret").Value;
            var key = Encoding.UTF8.GetBytes(secretKey); //TODO
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Creates a list of claims with the user name inside
        /// and all the roles the user belongs to.
        /// </summary>
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, this.user.UserName)
            };

            var roles = await this.userManager.GetRolesAsync(this.user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        /// <summary>
        /// Creates an object of the JwtSecurityToken type
        /// with all of the required options
        /// </summary>
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = this.configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}