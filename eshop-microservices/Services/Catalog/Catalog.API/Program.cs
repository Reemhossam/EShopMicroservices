
var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
//Add services to the container
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
//builder.Services.AddMarten(options =>
//{
//    options.Connection(builder.Configuration.GetConnectionString("Database")!);
//}).UseLightweightSessions();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
