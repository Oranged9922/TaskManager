using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {

        private readonly IValidator<TRequest>? _validator = validator;

        public async Task<TResponse> Handle(
           TRequest request,
           RequestHandlerDelegate<TResponse> next,
           CancellationToken cancellationToken)
        {
            if (validator is null)
            {
                return await next();
            }

            // before the handler
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (validationResult.IsValid)
            {
                return await next();
            }

            List<Error> errors = validationResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                     validationFailure.PropertyName,
                     validationFailure.ErrorMessage));

            return (dynamic)errors;
        }
    }
}
