
using BuyDozerBeMain.Application.UserEntitys.Commands.UpdateUserEntity;

public class UpdateUserEntityCommandValidator : AbstractValidator<UpdateUserEntityCommand>
{
    public UpdateUserEntityCommandValidator()
    {
        RuleFor(v => v.CompanyUser)
            .MaximumLength(25)
            .NotEmpty();
        RuleFor(v => v.PositionUser)
            .MaximumLength(50)
            .NotEmpty();
    }
}
