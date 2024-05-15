using System.Text.Json;
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.Common.Security;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.Commands.UpdatePaymentConfirmationReceiptTransaction;
[Authorize]

public record UpdatePaymentConfirmationReceiptTransactionCommand : IRequest<Response>
{
    public required string Id { get; init; }
    public required string PaymentConfirmationReceiptTransaction { get; init; }
}

public class UpdatePaymentConfirmationReceiptTransactionCommandHandler : IRequestHandler<UpdatePaymentConfirmationReceiptTransactionCommand, Response>
{
    private readonly IApplicationDbContext _context;

    public UpdatePaymentConfirmationReceiptTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(UpdatePaymentConfirmationReceiptTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Transactions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.PaymentConfirmationReceipt = request.PaymentConfirmationReceiptTransaction;
        entity.StatusTransaction = request.PaymentConfirmationReceiptTransaction != null ? 2 : 0; 

        await _context.SaveChangesAsync(cancellationToken);
        var response = new Response
        {
            Status = 200,
            Message = "Success",
            Data = entity
        };
        return response;
    }
}
