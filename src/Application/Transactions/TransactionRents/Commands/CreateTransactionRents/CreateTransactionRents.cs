using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Domain.Entities;

namespace BuyDozerBeMain.Application.Transactions.TransactionRents.Commands.CreateTransactionRents;
public record CreateTransactionCommand : IRequest<string>
{
    // public required string Id { get; init; }
    public string? TransactionNum { get; init; }
    public required HeavyUnit Unit { get; init; }
    public required UserEntity User { get; init; }
    public string? ReceiverName { get; init; }
    public string? ReceiverAddress { get; init; }
    public string? ReceiverHp { get; init; }
    public int QtyTransaction { get; init; }
    public decimal TotalPriceTransaction { get; init; }
    public DateOnly Date { get; init; }
    public int StatusTransaction { get; init; }
    public DateOnly DateReturn { get; init; }

}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Transaction
        {
            // Id = request.Id,
            TransactionNum = request.TransactionNum,
            Unit = request.Unit,
            User = request.User,
            ReceiverName = request.ReceiverName,
            ReceiverAddress = request.ReceiverAddress,
            ReceiverHp = request.ReceiverHp,
            QtyTransaction = request.QtyTransaction,
            TotalPriceTransaction = request.TotalPriceTransaction,
            DateTransaction = request.Date,
            StatusTransaction = request.StatusTransaction,
        };

        _context.Transactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var entity2 = new DetailRent
        {
            Transaction = entity,
            DateRent = request.Date,
            DateReturn = request.DateReturn
        };

        _context.DetailRents.Add(entity2);

        await _context.SaveChangesAsync(cancellationToken);
        //Tanya soal cara pas ngepost master sekalian detailnya dan udah dapet ID dari master
        //Soal UseCors Error
        //Penjelasan relational di Entity

        return entity.Id;
    }
}
