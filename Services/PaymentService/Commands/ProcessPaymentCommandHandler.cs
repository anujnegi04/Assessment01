using PaymentService.Data;
using PaymentService.Data.Entities;
using Shared.Contracts.Events;
using Shared.Messaging.EventBus;
using Microsoft.EntityFrameworkCore;
using PaymentService.Services;

namespace PaymentService.Commands
{
    public class ProcessPaymentCommandHandler
    {
        private readonly PaymentDbContext _context;
        private readonly IEventBus _eventBus;
        

        public ProcessPaymentCommandHandler(PaymentDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
           
        }

        public async Task<Payment> HandleAsync(ProcessPaymentCommand command)
        { 
            var existingPayment = await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == command.OrderId);

            if (existingPayment != null) {
                return existingPayment;
            }

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = command.OrderId,
                Amount = command.Amount,
                Status = "Succeeded",
                CorrelationId = command.CorrelationId,
                CreatedAtUtc = DateTime.UtcNow,
                ProcessedAtUtc = DateTime.UtcNow,
            };

            _context.Payments .Add(payment);
            await _context.SaveChangesAsync();

            await _eventBus.PublishAsync(new PaymentSucceededEvent
            {
                OrderId = payment.OrderId,
                PaymentID = payment.Id,
                Amount = payment.Amount,
                ProcessedAtUtc = payment.ProcessedAtUtc ?? DateTime.UtcNow,
                CorrelationId = payment.CorrelationId,
            });
                      
            return payment;
        }
    }
}
