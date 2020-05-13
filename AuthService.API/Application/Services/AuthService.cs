using AuthService.API.Application.Models.Authentication;
using AuthService.API.Application.Services.Interfaces;
using AuthService.Core.Helpers;
using AuthService.Domain.Accounts;
using AuthService.Domain.Exceptions;
using AuthService.RepoDB.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _config;

        public AuthService(IMemberRepository memberRepository, ILogger<AuthService> logger, IConfiguration config)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<TokenResponse> Authenticate(string email, string password)
        {
            var member = await _memberRepository.GetByEmailAsync(email);
            if (member?.CheckIsValidPassword(password) == true)
            {
                return new TokenResponse { AccessToken = GenerateToken(member.Id, DateTime.UtcNow.AddDays(7)), ExpiresIn = DateTime.UtcNow.AddDays(7).Ticks };
            }
            else 
            {
                throw new UnauthorizedException("Email o contraseña no válidos", "Unauthorized");
            }            
        }

        public void ValidateToken(string token)
        {
             if(!ValidateCurrentToken(token))
                throw new UnauthorizedException("Token no válido", "InvalidToken");
        }
        
        private string GenerateToken(Guid userId, DateTime? expiresIn)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://cloudservice.com";
            var myAudience = "http://cloudservice.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                }),
                Expires = expiresIn,
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool ValidateCurrentToken(string token)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://cloudservice.com";
            var myAudience = "http://cloudservice.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
