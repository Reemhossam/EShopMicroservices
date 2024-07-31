using Ordering.API;
using Ordering.Application;
using Ordering.Infrastucture;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastuctureServices(builder.Configuration);
builder.Services.AddApiServices();
var app = builder.Build();

//Configure the HTTP request pipeline.

app.Run();
