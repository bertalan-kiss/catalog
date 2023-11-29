using System.Data;
using Catalog.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Catalog.Api.Data;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web.UI;
using Microsoft.OpenApi.Models;

namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("CatalogApiContextConnection") ?? throw new InvalidOperationException("Connection string 'CatalogApiContextConnection' not found.");

        builder.Services.AddDbContext<CatalogApiContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CatalogApiContext>();
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
        var config = configurationBuilder.Build();

        builder.Services.AddControllers();
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationValidators();
        builder.Services.AddInfrastructureServices(config);

        builder.Services.AddMicrosoftIdentityWebApiAuthentication(config);
        builder.Services.AddEndpointsApiExplorer();

        //builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "OAuth2.0 Auth Code with PKCE",
                Name = "oauth2",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(config["AuthorizationUrl"]),
                        TokenUrl = new Uri(config["TokenUrl"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { config["ApiScope"], "read the api" }
                        }
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { config["ApiScope"] }
                }
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerAADdemo v1");
                c.OAuthClientId(config["OpenIdClientId"]);
                c.OAuthUsePkce();
                c.OAuthScopeSeparator(" ");
            });
        }

        app.MapControllers();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}

