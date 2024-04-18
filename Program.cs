global using FastEndpoints;
global using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = "The secret used to sign tokens") //add this
   .AddAuthorization();
builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthentication() //add this
   .UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//}
app.MapGet("/", () => "Hello World!");
app.UseFastEndpoints();
app.Run();
