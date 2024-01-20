
namespace Application.TOProjectLogic.Commands.CreateTOProject
{
    public class CreateTOProjectCommandValidator : AbstractValidator<CreateTOProjectCommand>
    {
        public CreateTOProjectCommandValidator() 
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(2000);
        }
    }
}
