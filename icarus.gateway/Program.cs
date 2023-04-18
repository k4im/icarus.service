using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var authenticationProviderKey = builder.Configuration["JWt:SecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(authenticationProviderKey, x => {
    x.Authority = builder.Configuration["Jwt:Issuer"];
    x.Audience = builder.Configuration["Jwt:Audience"];
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.Configuration = new OpenIdConnectConfiguration();  //<-- Most IMP Part
    x.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});
#region  configuração de ocelot
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, 
    reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
#endregion
var app = builder.Build();

app.UseDeveloperExceptionPage();
IdentityModelEventSource.ShowPII = true;

app.UseOcelot().Wait();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
