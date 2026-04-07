using Microsoft.EntityFrameworkCore;
using PaymentService.Commands;
using PaymentService.Data;
using PaymentService.Queries;
using PaymentService.Services;
using Shared.Contracts.Events;
using Shared.Messaging.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentDb")));

builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddScoped<ProcessPaymentCommandHandler>();
builder.Services.AddScoped<PaymentEventSubscriber>();
builder.Services.AddScoped<GetPaymentByOrderIdQueryHandler>();
builder.Services.AddHttpClient<FulfillmentClientService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

using (var scope = app.Services.CreateScope())
{
    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
    var subscriber = scope.ServiceProvider.GetRequiredService<PaymentEventSubscriber>();

    eventBus.Subscribe<OrderCreatedEvent>(subscriber.HandleOrderCreatedAsync);
}

    app.Run();
