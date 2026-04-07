using OrderService.Data;
using OrderService.Data.Entities;
using OrderService.Services;
using Shared.Contracts.Events;
using Shared.Messaging.EventBus;

namespace OrderService.Commands
{
    public class CreateOrderCommandHandler
    {
        private readonly OrderDbContext _context;
        private readonly IEventBus _eventBus;
        private readonly OrderSagaOrchestrator _orderSagaOrchestrator;
        public CreateOrderCommandHandler(OrderDbContext context,IEventBus eventBus,OrderSagaOrchestrator orderSagaOrchestrator)
        {
          _context = context;
          _eventBus = eventBus;
          _orderSagaOrchestrator = orderSagaOrchestrator;
        }

        public async Task<Order> HandleAsync(CreateOrderCommand command) {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerEmail = command.CustomerEmail,
                TotalAmount = command.TotalAmount,
                Status = "Pending",
                CreatedAtUtc = DateTime.UtcNow,
                CorrelationId = Guid.NewGuid().ToString()
            };

            _context.Orders.Add(order); 
            await _context.SaveChangesAsync();


            await _eventBus.PublishAsync(new OrderCreatedEvent{ 
            OrderId = order.Id,
            CustomerEmail = order.CustomerEmail,
            TotalAmount = order.TotalAmount,
            CreatedAtUtc = order.CreatedAtUtc,
            CorrelationId = order.CorrelationId
            });
         
            await _orderSagaOrchestrator.ProcessOrderAsync(order);
            return order;
        }
    }
}
