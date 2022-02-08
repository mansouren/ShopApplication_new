using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopApplication.Common;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ShopApplication.Services
{
    public class JwtService : IJwtService, IScopeDependency
    {

        private readonly SiteSettings siteSettings;

        public JwtService(IOptionsSnapshot<SiteSettings> siteSettings)

        {
            this.siteSettings = siteSettings.Value;

        }


        public Response GenerateToken(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            //var encryptKey = Encoding.UTF8.GetBytes(siteSettings.JwtSettings.EncryptKey);
            //var encryptedKey = new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = _getClaims(user);

            var Descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = siteSettings.JwtSettings.Issuer,
                Audience = siteSettings.JwtSettings.Audience,
                NotBefore = DateTime.UtcNow.AddMinutes(siteSettings.JwtSettings.NotBeforeMinutes),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(siteSettings.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptedKey

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(Descriptor);
            var jwt = tokenHandler.WriteToken(securityToken);
            string refreshToken =  CreateRefreshToken();
            return new Response 
            { 
                AccessToken = jwt,
                RefreshToken = refreshToken
            };
        }

        private IEnumerable<Claim> _getClaims(User user)
        {
            var list = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Mobile),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role ,user.Role.RoleName)

            };
            
            return list;
        }

        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber); // encrypt array of byte(0-255) digit numbers
                string token = Convert.ToBase64String(randomNumber);//encoding randomnumber to string with base64 algoritym
                return token;
            }
        }
        
       
    }
}
