using PaymentService.Commands;
using Shared.Contracts.Events;

namespace PaymentService.Services
{
    public class PaymentEventSubscriber
    {
        private readonly ProcessPaymentCommandHandler _processPaymentCommandHandler;

        public PaymentEventSubscriber(ProcessPaymentCommandHandler processPaymentCommandHandler)
        {
         _processPaymentCommandHandler = processPaymentCommandHandler;   
        }

        public async Task HandleOrderCreatedAsync(OrderCreatedEvent orderCreatedEvent)
        {
            var command = new ProcessPaymentCommand
            {
                OrderId = orderCreatedEvent.OrderId,
                Amount = orderCreatedEvent.TotalAmount,
                CorrelationId = orderCreatedEvent.CorrelationId
            };
            await _processPaymentCommandHandler.HandleAsync(command);
        }
    }
}
