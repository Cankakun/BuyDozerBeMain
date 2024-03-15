using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;

public class CreateHeavyUnitCommandValidator : AbstractValidator<CreateHeavyUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateHeavyUnitCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NameUnit)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.TypeUnit)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(v => v.QtyUnit)
            .NotEmpty();
        RuleFor(v => v.NameUnit)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueNameUnit)
                .WithMessage("'{PropertyName}' sudah ada!.")
                .WithErrorCode("Unique");

    }
    public async Task<bool> BeUniqueNameUnit(string nameUnit, CancellationToken cancellationToken)
    {
        return await _context.HeavyUnits
            .AllAsync(l => l.NameUnit != nameUnit, cancellationToken);
    }
}
