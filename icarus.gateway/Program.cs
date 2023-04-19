using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var urlAuth = builder.Configuration["Jwt:Issuer"];
var AuthenticationProviderKey = "teste";

builder.Services.AddAuthentication( opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(AuthenticationProviderKey, opt =>{
    opt.Authority = urlAuth;
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.Configuration = new OpenIdConnectConfiguration();
    opt.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime= false,
        ValidateIssuerSigningKey = false,
        

        ValidIssuer = urlAuth,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
}
);
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
