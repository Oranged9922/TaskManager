namespace Application.TOTaskLogic.Commands.CreateTOTask
{
    public class CreateTOTaskCommandValidator : AbstractValidator<CreateTOTaskCommand>
    {
        public CreateTOTaskCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(2000);
        }
    }
}
