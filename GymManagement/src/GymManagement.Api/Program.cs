using System.Text.Json.Serialization;
using GymManagement.Application;
using GymManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(config => config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.MapControllers();
app.UseEventualConsistencyMiddleware();

app.Run();