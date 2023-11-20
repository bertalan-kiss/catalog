using System.Data;
using Catalog.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
        var config = configurationBuilder.Build();

        builder.Services.AddControllers();
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationValidators();
        builder.Services.AddInfrastructureServices(config);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
        })
        .AddOpenIdConnect(options =>
        {
            options.Authority = "http://localhost:8080/realms/catalog";
            options.ClientId = "catalog_client";
            options.ClientSecret = "OtuKtluztaLGlE182ZYd2mI0wy2mJuHz";
            options.ResponseType = "code";
            options.SaveTokens = true;
            options.RequireHttpsMetadata = false;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.CallbackPath = "/signin-oidc"; // Set the callback path
            options.SignedOutCallbackPath = "/signout-callback-oidc"; // Set the signout callback path
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "catalog_user",
                RoleClaimType = "roles"
            };
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Add routes for callback handling
        app.Map("/signin-oidc", signinApp =>
        {
            signinApp.Run(async context =>
            {
                // Handle the callback from Keycloak after successful authentication
                await context.Response.WriteAsync("Authentication successful");
            });
        });

        app.Map("/signout-callback-oidc", signoutApp =>
        {
            signoutApp.Run(async context =>
            {
                // Handle the callback from Keycloak after sign-out
                await context.Response.WriteAsync("Sign-out successful");
            });
        });

        app.MapControllers();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=catalog}");

            // Add route for Keycloak authentication callback
            endpoints.MapControllerRoute(
                name: "login-callback",
                pattern: "login-callback",
                defaults: new { controller = "Account", action = "LoginCallback" });
        });

        app.Run();
    }
}

