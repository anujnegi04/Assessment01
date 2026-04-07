using FulfillmentService.Commands;
using FulfillmentService.Data;
using FulfillmentService.Queries;
using FulfillmentService.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FulfillmentDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("FulfillmentDb")));

builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
builder.Services.AddScoped<CreateFulfillmentCommandHandler>();
builder.Services.AddScoped<FulfillmentEventSubscriber>();
builder.Services.AddScoped<GetFulfillmentByOrderQueryHandler>();

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
