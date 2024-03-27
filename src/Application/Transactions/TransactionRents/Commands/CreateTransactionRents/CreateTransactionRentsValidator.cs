using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.Transactions.TransactionRents.Commands.CreateTransactionRents;

public class CreateDetailRentCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateDetailRentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.ReceiverName)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.ReceiverAddress)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.ReceiverHp)
            .MaximumLength(13)
            .NotEmpty();
        RuleFor(v => v.QtyTransaction)
            .NotEmpty();
        RuleFor(v => v.TotalPriceTransaction)
            .NotEmpty();
        RuleFor(v => v.StatusTransaction)
            .NotEmpty();


    }
}
