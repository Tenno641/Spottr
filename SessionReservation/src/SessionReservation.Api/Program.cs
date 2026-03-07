using SessionReservation.Application;
using SessionReservation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

RouteGroupBuilder participants = app.MapGroup("api/participants");


app.Run();