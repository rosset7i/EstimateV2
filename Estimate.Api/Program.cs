using Estimate.Application;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.IoC;
using Rossetti.Common.Configuration;
using Rossetti.Common.Configuration.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddContext(builder.Configuration.GetConnectionString("Default")!);
    builder.Services.AddRepositories();
    builder.Services.AddUnitOfWork<EstimateDbContext>();
    builder.Services.AddCaching(builder.Configuration.GetConnectionString("Redis")!);
    builder.Services.AddSwagger("Estimate");
    builder.Services.AddMediator<IAssemblyMarker>();
    builder.Services.AddValidators<IAssemblyMarker>();
    builder.Services.AddErrorHandler();
}

var app = builder.Build();
{
    app.UseSwaggerMiddleware();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseBusinessExceptionMiddleware();
    app.Run();
}