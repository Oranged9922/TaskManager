using Application.Common.Interfaces;
using Domain.Enums.User;
using Domain.UserAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Authentication
{
    /// <summary>
    /// Jwt Token Generator class.
    /// </summary>
    public sealed class JwtTokenGenerator
        (
        IOptions<JwtSettings> jwtOptions
        ) : IJwtTokenGenerator
    {
        private readonly JwtSettings jwtSettings = jwtOptions.Value;

        public string GenerateToken(User user, UserRole role)
        {
            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim(JwtRegisteredClaimNames.Sub, user!.Id!.Value.ToString()!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Typ, role.ToString())
            ];

            JwtSecurityToken secToken = new(
                issuer: jwtSettings.Issuer,
                expires: DateTime.Now.AddMinutes(jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials,
                audience: jwtSettings.Audience);

            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }
    }
}
