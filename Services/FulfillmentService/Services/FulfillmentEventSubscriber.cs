using FulfillmentService.Commands;
using Shared.Contracts.Events;

namespace FulfillmentService.Services
{
    public class FulfillmentEventSubscriber
    {
        private readonly CreateFulfillmentCommandHandler _createFulfillmentCommandHandler;
        public FulfillmentEventSubscriber(CreateFulfillmentCommandHandler createFulfillmentCommandHandler)
        {
            _createFulfillmentCommandHandler = createFulfillmentCommandHandler;
        }

        public async Task HandlePaymentSucceededAsync(PaymentSucceededEvent paymentSucceededEvent)
        {
            var command = new CreateFulfillmentCommand
            {
                OrderId = paymentSucceededEvent.OrderId,
                PaymentId = paymentSucceededEvent.PaymentID,
                CorrelationId = paymentSucceededEvent.CorrelationId
            };

            await _createFulfillmentCommandHandler.HandleAsync(command);
        }
    }
}
