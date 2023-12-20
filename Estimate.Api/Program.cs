using Estimate.Api.ErrorHandling;
using Estimate.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddContext(builder.Configuration);
    builder.Services.AddAuthentication(builder.Configuration);
    builder.Services.AddAuthorization();
    builder.Services.AddSwagger();
    builder.Services.AddMediator();
    builder.Services.AddRepositories();
    builder.Services.AddValidators();
    builder.Services.AddCaching(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("swagger-ui");
        c.OAuthClientSecret("swagger-ui-secret");
        c.OAuthRealm("swagger-ui-realm");
        c.OAuthAppName("Swagger UI");
    });
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILoggerProvider>());
    app.Run();
}