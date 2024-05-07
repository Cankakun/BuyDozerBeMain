namespace BuyDozerBeMain.Application.PriceListRents.Queries.GetPriceListRent;

public class GetPriceListRentsValidator : AbstractValidator<GetPriceListRentsQuery>
{
    public GetPriceListRentsValidator()
    {
        // RuleFor(x => x.NameUnit)
        //     .NotEmpty().WithMessage("ListId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
