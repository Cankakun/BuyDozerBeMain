namespace BuyDozerBeMain.Application.HeavyUnits.Commands.UpdateHeavyUnit;

public class UpdateHeavyUnitCommandValidator : AbstractValidator<UpdateHeavyUnitCommand>
{
    public UpdateHeavyUnitCommandValidator()
    {
        RuleFor(v => v.NameUnit)
            .MaximumLength(200)
            .NotEmpty();
    }
}
