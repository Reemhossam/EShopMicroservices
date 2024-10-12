using BulidingBlocks.Exceptions.Handler;
using BulidingBlocks.Messaging.MassTransit;
using Discount.Grpc;

var builder = WebApplication.CreateBuilder(args);

//add services to the container.
//Application services
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

//Data services
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Grpc services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address= new Uri(builder.Configuration["GrpcSetting:DiscountUrl"]!);
});

//Async Communication services
builder.Services.AddMessageBroker(builder.Configuration);

//Cross Cutting services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
//configure the HTTP request pipeline.

app.Run();
