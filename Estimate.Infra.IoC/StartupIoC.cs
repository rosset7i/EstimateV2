using System.Text;
using Estimate.Application;
using Estimate.Application.Common;
using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories;
using Estimate.Infra.Repositories.Base;
using Estimate.Infra.TokenFactory;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Estimate.Infra.IoC;

public static class StartupIoC
{
    public static void AddContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<EstimateDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<EstimateDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Secret").Value!))
                };
            });

        services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseContext, EstimateDbContext>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IEstimateRepository, EstimateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddCaching(this IServiceCollection service, ConfigurationManager configuration)
    {
        service.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = configuration.GetConnectionString("Redis");
        });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Estimate's API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "Bearer {Token}",
                Name = "Authorization",
                Scheme = "bearer",
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            c.CustomSchemaIds(x => x.FullName);
        });
    }
}
