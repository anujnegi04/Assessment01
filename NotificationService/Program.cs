using NotificationService.Services;
using Shared.Contracts.Events;
using Shared.Messaging.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddScoped<OrderCreatedNotificationHandler>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope()) {

    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
    var handler = scope.ServiceProvider.GetRequiredService<OrderCreatedNotificationHandler>();

    eventBus.Subscribe<OrderCreatedEvent>(handler.HandleAsync);
}

    app.Run();
