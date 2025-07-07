using Inventory.ServerLogic.Utils;
using Inventory.WebAPI;
using Inventory.WebAPI.Middlewares;
using Inventory.WebAPI.Services;
using Inventory.WebAPI.Utils;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// services
var configs = builder.Configuration;
builder.Services.AddScoped<UserInfo>();
builder.Services.AddAllServices(configs);

builder.Services.AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    var options = configs.GetSection("authOptions");

                    var secret = options["secret"];
                    ArgumentNullException.ThrowIfNull(secret);

                    o.TokenValidationParameters = new()
                    {
                        ValidIssuer = options["issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                                    Encoding.UTF8.GetBytes(secret))
                    };
                    
                });

builder.Services.AddAuthorization();

builder.Services.AddAuthServices(new()
{
    Endpoint = configs.GetRequiredConfig("auth::Endpoint"),
    HeaderKey = configs.GetRequiredConfig("auth::Key")
});

builder.Services.AddCors(options =>
{
    var origins = configs["cors"];
    if (origins is not null)
    {

        var urls = origins.Split(",");

        options.AddDefaultPolicy(builder =>
            builder.WithOrigins(urls)
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    }

});


var app = builder.Build();

app.UseCors();
// Middlewares

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<UserMiddleware>();

// Endpoints
app.MapGet("/", () => "Hello Inventory World!");

app.HandleExceptions();

app.MapAllEndpoints();


await app.RunAsync();
