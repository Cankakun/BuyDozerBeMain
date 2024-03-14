namespace BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;

public class CreateHeavyUnitCommandValidator : AbstractValidator<CreateHeavyUnitCommand>
{
    public CreateHeavyUnitCommandValidator()
    {
        RuleFor(v => v.NameUnit)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.TypeUnit)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(v => v.QtyUnit)
            .NotEmpty();

    }
}
