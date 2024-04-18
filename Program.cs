global using FastEndpoints;
global using FastEndpoints.Security;
using ApiWithFastEndpoints.Model;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = "The secret used to sign tokens") //add this
   .AddAuthorization();
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .Configure<JWTModel>(
        builder.Configuration.GetSection("JwtToken"));
//builder.Configuration.GetRequiredSection(nameof(JWTModel)).Bind(mySettings);

var app = builder.Build();
app.UseAuthentication() //add this
   .UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.MapGet("/", () => "Hello World!");
app.UseFastEndpoints();
app.Run();


