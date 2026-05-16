using FluentValidation;
using Transparity.Application.Abstractions;
using Transparity.Shared.Attributes;

namespace Transparity.Application.Behaviors {
    [PipelineOrder(1)]
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
            _validators = validators;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next) {
            if (_validators.Any()) {
                foreach (var validator in _validators) {
                    await validator.ValidateAndThrowAsync(request);
                }
            }

            return await next();
        }
    }
}
