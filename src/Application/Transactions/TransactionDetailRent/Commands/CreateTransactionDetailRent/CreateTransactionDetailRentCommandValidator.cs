using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailRent.Commands.CreateTransactionDetailRent;
public class CreateTransactionDetailRentCommandValidator : AbstractValidator<CreateTransactionDetailRentCommand>
{

    public CreateTransactionDetailRentCommandValidator()
    {

        // RuleFor(v => v.TransactionNum)
        //     .MaximumLength(20)
        //     .NotEmpty();
        RuleFor(v => v.UnitId)
            .NotEmpty();
        RuleFor(v => v.UserId)
            .NotEmpty();
        RuleFor(v => v.ReceiverName)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.ReceiverHp)
            .MaximumLength(13)
            .NotEmpty();
        RuleFor(v => v.ReceiverAddress)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.QtyTransaction)
            .NotEmpty();
        RuleFor(v => v.DateTransaction)
            .NotEmpty();
        RuleFor(v => v.StatusTransaction)
            .NotEmpty();

    }

}
