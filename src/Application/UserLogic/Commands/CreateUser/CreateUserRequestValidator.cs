using Contracts.User.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserLogic.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).ChildRules(password =>
            {
                password.RuleFor(x => x).Must(x => x.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter");
                password.RuleFor(x => x).Must(x => x.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter");
                password.RuleFor(x => x).Must(x => x.Any(char.IsDigit)).WithMessage("Password must contain at least one digit");
                password.RuleFor(x => x).Must(x => x.Any(char.IsSymbol)).WithMessage("Password must contain at least one symbol");
            });
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
