using FluentValidation;
using FluentValidation.Results;
using Transparity.Application.Abstractions;
using Transparity.Shared.Attributes;
using Transparity.Shared.Constants;

namespace Transparity.Application.Behaviors {
    [PipelineOrder(1)]
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
            _validators = validators;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next) {
            if (request is null) {
                var propName = typeof(TRequest).Name;
                var errMessage = string.Format(
                    ErrorMessageConstants.ArgErrMessage, propName);

                var finalErrMessage = string.Format("{0} {1}", 
                    errMessage, ErrorMessageConstants.ArgCannotBeNull);

                var failure = new ValidationFailure(propName, finalErrMessage);
                throw new ValidationException(finalErrMessage, [failure]);
            }

            if (_validators.Any()) {
                foreach (var validator in _validators) {
                    await validator.ValidateAndThrowAsync(request);
                }
            }

            return await next();
        }
    }
}
