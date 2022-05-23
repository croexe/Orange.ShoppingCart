using Serilog;
using ShoppingCart.BusinessLogic.Implementation;
using ShoppingCart.BusinessLogic.Interfaces;
using ShoppingCart.Infrastructure.Database;
using ShoppingCart.Utillity.Profiles;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(OrderProfile), typeof(OrderItemProfile));

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc

.ReadFrom.Configuration(ctx.Configuration)));

builder.Services.AddScoped<IOrderService, OrderRepository>();
builder.Services.AddDbContext<CartDbContext>();
var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
