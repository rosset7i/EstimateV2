using System.Text;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Application.Infrastructure;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories;
using Estimate.Infra.Repositories.Base;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<EstimateDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuthentication(options =>
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
    }

    public static void AddQueries(this IServiceCollection services)
    {
    }

    public static void AddStores(this IServiceCollection services)
    {
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

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        ValidatorOptions.Global.LanguageManager.Enabled = false;
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