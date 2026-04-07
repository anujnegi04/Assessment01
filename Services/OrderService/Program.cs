using Microsoft.EntityFrameworkCore;
using OrderService.Commands;
using OrderService.Data;
using OrderService.Queries;
using OrderService.Services;
using Shared.Messaging.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<OrderDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));
builder.Services.AddScoped<CreateOrderCommandHandler>();
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddScoped<GetOrderByIdQueryHandler>();
builder.Services.AddHttpClient<OrderSagaOrchestrator>();
builder.Services.AddScoped<GetAllOrdersQueryHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
