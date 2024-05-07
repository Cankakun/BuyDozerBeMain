using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.HeavyUnits.Commands.CreateHeavyUnit;

public class CreateHeavyUnitCommandValidator : AbstractValidator<CreateHeavyUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateHeavyUnitCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NameUnit)
            .Length(3, 50)
            .WithMessage("Nama Unit kurang dari 3 karakter atau lebih dari 50 karakter")
            .NotEmpty()
            .WithMessage("Nama Unit tidak boleh kosong!")
            .MustAsync(BeUniqueNameUnit)
            .WithMessage("'{PropertyName}' sudah ada!.")
            .WithErrorCode("Unique");
        RuleFor(v => v.TypeUnit)
            .Length(3, 25)
            .WithMessage("Tipe Unit kurang dari 3 karakter atau lebih dari 50 karakter")
            .NotEmpty()
            .WithMessage("Tipe Unit tidak boleh kosong!");
        RuleFor(v => v.QtyUnit)
            .GreaterThan(0)
            .WithMessage("Qty Unit tidak boleh kurang dari 1!")
            .NotEmpty()
            .WithMessage("Qty Unit tidak boleh kosong!");
        RuleFor(v => v.PriceRentUnit)
            .GreaterThan(0)
            .WithMessage("PriceRentUnit tidak boleh kurang dari 1!")
            .NotEmpty()
            .WithMessage("PriceRentUnit tidak boleh kosong!");
        RuleFor(v => v.PriceBuyUnit)
            .GreaterThan(0)
            .WithMessage("PriceBuyUnit tidak boleh kurang dari 1!")
            .NotEmpty()
            .WithMessage("PriceBuyUnit tidak boleh kosong!");
    }
    public async Task<bool> BeUniqueNameUnit(string nameUnit, CancellationToken cancellationToken)
    {
        return await _context.HeavyUnits
            .AllAsync(l => l.NameUnit != nameUnit, cancellationToken);
    }
}
