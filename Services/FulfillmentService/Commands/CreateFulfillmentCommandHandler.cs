using FulfillmentService.Data;
using FulfillmentService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FulfillmentService.Commands
{
    public class CreateFulfillmentCommandHandler
    {
        private readonly FulfillmentDbContext _context;

        public CreateFulfillmentCommandHandler(FulfillmentDbContext context) {
            _context = context;
        }

        public async Task<Fulfillment> HandleAsync(CreateFulfillmentCommand command)
        {
            var existingFulfillment = await _context.Fulfillments
                .FirstOrDefaultAsync(f => f.OrderId == command.OrderId);

            if (existingFulfillment != null)
            {
                return existingFulfillment;
            }

            var fulfillment = new Fulfillment
            {
                Id = Guid.NewGuid(),
                OrderId = command.OrderId,
                PaymentId = command.PaymentId,
                Status = "Created",
                CorrelationId = command.CorrelationId,
                CreatedAtUtc = DateTime.UtcNow
            };

            _context.Fulfillments.Add(fulfillment);
            await _context.SaveChangesAsync();

            return fulfillment;
        }
    }
}
