using FluentValidation;

namespace Application.UserLogic.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Your e-mail address is not valid.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8 symbols long.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16 symbols.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Your username cannot be empty")
                .MaximumLength(24).WithMessage("Your username length must not exceed 24 symbols long.")
                .MinimumLength(3).WithMessage("Your username length must be at least 3 symbols long.");
        }
    }
}
