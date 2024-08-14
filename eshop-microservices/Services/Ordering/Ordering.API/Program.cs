using Ordering.API;
using Ordering.Application;
using Ordering.Infrastucture;
using Ordering.Infrastucture.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastuctureServices(builder.Configuration);
builder.Services.AddApiServices();
var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
