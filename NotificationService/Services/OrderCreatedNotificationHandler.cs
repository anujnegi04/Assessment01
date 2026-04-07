using Shared.Contracts.Events;

namespace NotificationService.Services
{
    public class OrderCreatedNotificationHandler
    {
        public Task HandleAsync(OrderCreatedEvent orderCreatedEvent)
        {
            Console.WriteLine("NotificationService Received OrderCreated Event");
            Console.WriteLine($"OrderId:{orderCreatedEvent.OrderId}");
            Console.WriteLine($"CustomerEmail:{orderCreatedEvent.CustomerEmail}");
            Console.WriteLine($"TotalAmount:{orderCreatedEvent.TotalAmount}");
 
            return Task.CompletedTask;  
        }
    }
}
