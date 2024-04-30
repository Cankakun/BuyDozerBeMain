using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.Transactions.TransactionDetailBuy.Commands.CreateTransactionDetailBuy;
public class CreateTransactionDetailBuyCommandValidator : AbstractValidator<CreateTransactionDetailBuyCommand>
{

    public CreateTransactionDetailBuyCommandValidator()
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
