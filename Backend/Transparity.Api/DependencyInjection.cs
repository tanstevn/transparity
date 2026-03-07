using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using System.Diagnostics.CodeAnalysis;
using Transparity.Application.Abstractions;
using Transparity.Application.Healths.Checks;
using Transparity.Data;
using Transparity.Infrastructure.Mediator;
using Transparity.Shared.Models;

namespace Transparity.Api {
    public class DependencyInjection {
        public static void ConfigureConfiguration(IConfigurationManager config) {
            config.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration config) {
            services.AddOpenApi(options => {
                options.AddDocumentTransformer((document, context, _) => {
                    document.Info = new() {
                        Title = "Transparity API documentation",
                        Version = "v1"
                    };

                    return Task.CompletedTask;
                });
            });

            var connectionString = config
                .GetConnectionString("Neon")!;

            services.AddHealthChecks()
                .AddCheck<AppHealthCheck>(name: "Transparity")
                .AddNpgSql(connectionString, name: "Neon");

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSingleton<AppState>();
            services.AddMediatorFromAssembly(typeof(IMediator).Assembly);
            services.AddValidatorsFromAssembly(typeof(IMediator).Assembly);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        [SuppressMessage("Usage", "ASP0014:Suggest using top level route registrations", 
            Justification = "app.UseEndpoints(...): This is just to handle the " +
            "fallback logic when someone tries to search API routes randomly")]
        public static void ConfigureApplication(WebApplication app, IWebHostEnvironment env) {
            if (!env.IsEnvironment("Production")) {
                app.MapOpenApi();
                app.MapScalarApiReference();

                app.MapGet("/", () => Results.Redirect("/scalar/v1"))
                    .ExcludeFromDescription();
            }

            app.UseCors();

            // Add middleware here that "IS NOT" endpoint/route context reliant

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapFallback(context => {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    return context.Response
                        .WriteAsJsonAsync(string.Empty);
                });
            });

            // Add middleware here that "IS" endpoint/route context reliant

            using var scope = app.Services
                .CreateScope();

            //scope.ServiceProvider
            //    .GetRequiredService<ApplicationDbContext>()
            //    .Database
            //    .Migrate();

            var appState = scope.ServiceProvider
                .GetRequiredService<AppState>();

            app.Lifetime.ApplicationStarted.Register(() => {
                appState.IsReady = true;
                appState.IsAlive = true;
            });
        }
    }
}
