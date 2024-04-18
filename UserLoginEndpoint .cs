using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Paket;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiWithFastEndpoints
{
    public class UserLoginEndpoint : Endpoint<LoginRequest>
    {
        public override void Configure()
        {
            Post("/api/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken cancellationToken)
        {
            //var jwtToken = JwtBearer.CreateToken(
            //    o =>
            //    {
            //        o.SigningKey = "A secret token signing key";
            //        o.ExpireAt = DateTime.UtcNow.AddDays(1);
            //        o.User.Roles.Add("Manager", "Auditor");
            //        o.User.Claims.Add(("Email", req.Email));
            //        o.User["UserId"] = "001"; //indexer based claim setting
            //    });
            var jwtToken = BuildJWTToken();
            await SendAsync(
                new
                {
                    req.Email,
                    Token = jwtToken
                });

        }

        private string BuildJWTToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeywqewqeqqqqqqqqqqqweeeeeeeeeeeeeeeeeeeqweqe"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = "https://localhost:7146";
            var audience = "https://localhost:7146";
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble("60"));
            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
