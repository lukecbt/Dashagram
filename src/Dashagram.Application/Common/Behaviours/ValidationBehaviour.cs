using FluentValidation;
using MediatR;

namespace Dashagram.Application.Common.Behaviours
{
    /// <summary>
    /// This class is responsible for validating requests before they reach the request handler.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        // Validators for the current request
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                // Run all validators asynchronously and collect the results
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Count != 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Count != 0)
                {
                    // If there are validation failures, throw a ValidationException with the list of failures to be handled by the exception handler
                    throw new ValidationException(failures);
                }
            }

            return await next(cancellationToken);
        }
    }
}
