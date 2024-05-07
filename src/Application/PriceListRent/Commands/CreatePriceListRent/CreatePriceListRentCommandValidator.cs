using BuyDozerBeMain.Application.Common.Interfaces;

namespace BuyDozerBeMain.Application.PriceListRents.Commands.CreatePriceListRent;

public class CreatePriceRentUnitCommandValidator : AbstractValidator<CreatePriceListRentCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePriceRentUnitCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.NameRent)
            .Length(3, 50)
            .WithMessage("Nama Sewa kurang dari 3 karakter atau lebih dari 50 karakter")
            .NotEmpty()
            .WithMessage("Nama Sewa tidak boleh kosong!")
            .MustAsync(BeUniqueNameSewa)
            .WithMessage("'{PropertyName}' sudah ada!.")
            .WithErrorCode("Unique");

        RuleFor(v => v.PriceRentUnit)
            .GreaterThan(0)
            .WithMessage("Qty Unit tidak boleh kurang dari 1!")
            .NotEmpty()
            .WithMessage("Qty Unit tidak boleh kosong!");
        RuleFor(v => v.Months)
            .GreaterThan(0)
            .WithMessage("Months tidak boleh kurang dari 1!")
            .NotEmpty()
            .WithMessage("Months tidak boleh kosong!");

    }
    public async Task<bool> BeUniqueNameSewa(string nameRent, CancellationToken cancellationToken)
    {
        return await _context.PriceListRents
            .AllAsync(l => l.NameRent != nameRent, cancellationToken);
    }
}
