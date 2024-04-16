using BulidingBlocks.Behaviors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
//builder.Services.AddMarten(options =>
//{
//    options.Connection(builder.Configuration.GetConnectionString("Database")!);
//}).UseLightweightSessions();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();

app.Run();
