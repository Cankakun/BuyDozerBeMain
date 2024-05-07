namespace BuyDozerBeMain.Application.PriceListRents.Commands.UpdatePriceListRent;

public class UpdatePriceListRentCommandValidator : AbstractValidator<UpdatePriceListRentCommand>
{
    public UpdatePriceListRentCommandValidator()
    {
        RuleFor(v => v.NameRent)
            .MaximumLength(200)
            .NotEmpty();
    }
}
