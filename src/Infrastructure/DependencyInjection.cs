using System.Reflection.Metadata;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Domain.Constants;
using BuyDozerBeMain.Domain.Entities;
using BuyDozerBeMain.Infrastructure.Data;
using BuyDozerBeMain.Infrastructure.Data.Interceptors;
using BuyDozerBeMain.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        // services.AddAuthentication()
        //     .AddBearerToken(props =>
        //     {
        //         props.BearerTokenExpiration = TimeSpan.FromHours(3);

        //     });
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme, (props) =>
            {
                props.BearerTokenExpiration = TimeSpan.FromHours(3);
            });

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<UserEntity>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
