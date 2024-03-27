
using BuyDozerBeMain.Application.Common.Interfaces;
using BuyDozerBeMain.Application.UserEntitys.Commands.RegisterUserEntity;

public class RegisterUserEntityCommandValidator : AbstractValidator<RegisterUserEntityCommand>
{
    private readonly IApplicationDbContext _context;
    public RegisterUserEntityCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.UserName)
            .MaximumLength(50)
            .NotEmpty()
            .MustAsync(BeUniqueUserName)
            .WithMessage("'{PropertyName}' sudah ada!.")
            .WithErrorCode("Unique");
        RuleFor(v => v.Email)
            .MaximumLength(60)
            .NotEmpty()
            .MustAsync(BeUniqueEmail)
            .WithMessage("'{PropertyName}' sudah ada!.")
            .WithErrorCode("Unique");
        RuleFor(v => v.Password)
            .NotEmpty();
        RuleFor(v => v.CompanyUser)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(v => v.PositionUser)
            .MaximumLength(50)
            .NotEmpty();
    }
    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.UserEntitys
            .AllAsync(l => l.Email != email, cancellationToken);
    }
    public async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await _context.UserEntitys
            .AllAsync(l => l.UserName != userName, cancellationToken);
    }
}
