using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {

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
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
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
