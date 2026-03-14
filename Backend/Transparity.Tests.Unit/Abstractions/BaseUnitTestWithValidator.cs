using FluentValidation;
using Transparity.Application.Abstractions;

namespace Transparity.Tests.Unit.Abstractions {
    public abstract class BaseUnitTestWithValidator<TTestClass, TRequest, TResponse, TRequestHandler, TRequestValidator>
        : BaseUnitTest<TTestClass, TRequest, TResponse, TRequestHandler>
        where TTestClass : BaseUnitTestWithValidator<TTestClass, TRequest, TResponse, TRequestHandler, TRequestValidator>
        where TRequest : IRequest<TResponse>
        where TRequestHandler : class, IRequestHandler<TRequest, TResponse>
        where TRequestValidator : class, IValidator<TRequest> {
        protected TRequestValidator _validator = default!;

        protected override void SetupRequestHandler() {
            _validator = Activator.CreateInstance<TRequestValidator>();
            _requestHandler = CreateRequestHandler();
        }

        protected override TTestClass Act() {
            var validationResult = _validator.Validate(_request);

            if (!validationResult.IsValid) {
                _exception = new ValidationException(validationResult.Errors);
                return (TTestClass)this;
            }

            return base.Act();
        }
    }
}
