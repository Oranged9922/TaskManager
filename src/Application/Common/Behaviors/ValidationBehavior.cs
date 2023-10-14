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

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return next();
            }

            FluentValidation.Results.ValidationResult validationResult = _validator.Validate(request);

            if (validationResult.IsValid)
            {
                return next();
            }

            List<Error> errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));

            return (dynamic)errors;
        }
    }
}
