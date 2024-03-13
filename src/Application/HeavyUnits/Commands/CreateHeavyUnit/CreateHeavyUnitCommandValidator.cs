namespace BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;

public class CreateHeavyUnitCommandValidator : AbstractValidator<CreateHeavyUnitCommand>
{
    public CreateHeavyUnitCommandValidator()
    {
        RuleFor(v => v.NameUnit)
            .MaximumLength(200)
            .NotEmpty();

    }
}
