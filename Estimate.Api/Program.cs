using Estimate.Application;
using Estimate.Infra.IoC;
using Rossetti.Common.Configuration;
using Rossetti.Common.Configuration.Middleware;
using Rossetti.Common.ErrorHandler.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddContext(builder.Configuration);
    builder.Services.AddAuthentication(builder.Configuration);
    builder.Services.AddAuthorization();
    builder.Services.AddRepositories();
    builder.Services.AddCaching(builder.Configuration);
    //Migrate
    builder.Services.AddSwagger("Estimate");
    builder.Services.AddMediator<IAssemblyMarker>();
    builder.Services.AddValidators<IAssemblyMarker>();
    builder.Services.AddErrorWrapper();
}

var app = builder.Build();
{
    app.UseSwaggerMiddleware();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseBusinessExceptionHandler();
    app.Run();
}