namespace BuyDozerBeMain.Application.HeavyUnits.Queries.GetHeavyUnit;

public class GetHeavyUnitsValidator : AbstractValidator<GetHeavyUnitsQuery>
{
    public GetHeavyUnitsValidator()
    {
        // RuleFor(x => x.NameUnit)
        //     .NotEmpty().WithMessage("ListId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
