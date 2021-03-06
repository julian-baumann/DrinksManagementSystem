using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackendServer.RestApi.V1
{
    [Route("api/v1/authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorizationController(IConfiguration configuration) {
            _configuration = configuration; // Needed to access the stored  JWT secret key
        }

        [HttpPost]
        public IActionResult GenerateTokenAsymmetric() {
            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey( // Convert the loaded key from base64 to bytes.
                source: Convert.FromBase64String(_configuration["Jwt:Asymmetric:PrivateKey"]), // Use the private key to sign tokens
                bytesRead: out var _); // Discard the out variable

            var signingCredentials = new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256 // Important to use RSA version of the SHA algo
            );

            var jwtDate = DateTime.Now;

            var jwt = new JwtSecurityToken(
                audience: "jwt-test",
                issuer: "jwt-test",
                claims: new[] {new Claim(ClaimTypes.NameIdentifier, "some-username")},
                notBefore: jwtDate,
                expires: jwtDate.AddSeconds(10),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                jwt = token,
                unixTimeExpiresAt = new DateTimeOffset(jwtDate).ToUnixTimeMilliseconds(),
            });
        }

        [HttpGet]
        [Authorize] // Use the "Asymmetric" authentication scheme
        public IActionResult ValidateTokenAsymmetric()
        {
            return Ok();
        }
    }
}