using Estimate.Application;
using Estimate.Infra.IoC;
using Rossetti.Common.Configuration;
using Rossetti.Common.ErrorHandler.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddContext(builder.Configuration);
    builder.Services.AddAuthentication(builder.Configuration);
    builder.Services.AddAuthorization();
    builder.Services.AddSwagger();
    builder.Services.AddRepositories();
    builder.Services.AddCaching(builder.Configuration);
    //Migrate
    builder.Services.AddMediator<IAssemblyMarker>();
    builder.Services.AddValidators<IAssemblyMarker>();
    builder.Services.AddErrorWrapper();
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
    //Migrated
    app.UseBusinessExceptionHandler();
    //Migrated
    app.Run();
}