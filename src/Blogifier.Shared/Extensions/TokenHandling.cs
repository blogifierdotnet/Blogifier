
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Blogifier.Shared.Extensions
{
    public static class TokenHandling
	{
        public static string GenerateToken(string salt, string PreferredUsername)
        {
            var mySecret = salt;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, PreferredUsername),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                //Issuer = myIssuer, //set if multi-tenant
                //Audience = myAudience, //set if multi-tenant
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
        public static async Task<string> ValidateCurrentToken(string salt, string token)
        {
            var mySecret = salt;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));


            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false, //set to true if multi-tenant
                    ValidateAudience = false, //set to true if multi-tenant
                    //ValidIssuer = myIssuer, //set if multi-tenant
                    //ValidAudience = myAudience, //set if multi-tenant
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
				// if we got this far then the token must be valid
                return await Task.FromResult(GetUserIdFromClaim(token));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
        public static string GetUserIdFromClaim(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == "nameid").Value;
            return stringClaimValue;
        }
    }
}