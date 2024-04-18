using ApiWithFastEndpoints.Model;
using FastEndpoints;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paket;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiWithFastEndpoints
{
    public class UserLoginEndpoint : Endpoint<LoginRequest>
    {
        private readonly JWTModel _appSetting;

        public UserLoginEndpoint(IOptions<JWTModel> settings)
        {
            _appSetting = settings.Value;
        }
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
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>(
            //    "JwtToken:SecretKey")));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _appSetting.Issuer;
            var audience = _appSetting.Audience;
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(_appSetting.TokenExpiry));
            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
