using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.Transactions.Commands.UpdatePaymentConfirmationReceiptTransaction;
public class UpdatePaymentConfirmationReceiptTransactionValidator : AbstractValidator<UpdatePaymentConfirmationReceiptTransactionCommand>
{

    public UpdatePaymentConfirmationReceiptTransactionValidator()
    {

        RuleFor(v => v.PaymentConfirmationReceiptTransaction)
            .NotEmpty()
            .WithMessage("Bukti pembayaran tidak boleh kosong!");
    }

}
