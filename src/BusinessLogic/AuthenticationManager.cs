using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreExampleAuth.Entities.Models;
using NetCoreExampleAuth.Patterns.Configs;
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
        IOptions<JwtSettings> jwtSettings;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.jwtSettings = jwtSettings;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationContract userForAuth)
        {
            var user = await this.userManager.FindByNameAsync(userForAuth.UserName);

            return (user != null && await this.userManager.CheckPasswordAsync(user, userForAuth.Password));
        }

        public async Task<string> CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <returns>Returns secret key as a byte array with the security algorithm.</returns>
        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = this.jwtSettings.Value.Secret;
            var key = Encoding.UTF8.GetBytes(secretKey); //TODO
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Creates a list of claims with the user name inside
        /// and all the roles the user belongs to.
        /// </summary>
        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await this.userManager.GetRolesAsync(user);

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
            var tokenOptions = new JwtSecurityToken
            (
                issuer: this.jwtSettings.Value.ValidIssuer,
                audience: this.jwtSettings.Value.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(this.jwtSettings.Value.Expires)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}