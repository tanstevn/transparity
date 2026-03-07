using FluentValidation;
using FluentValidation.Results;
using Moq;
using Transparity.Application.Abstractions;

namespace Transparity.Tests.Unit.Abstractions {
    internal class BaseUnitTestWithValidator<TTestClass, TRequest, TResponse, TRequestHandler, TRequestValidator> : BaseUnitTest<TTestClass, TRequest, TResponse, TRequestHandler>
        where TTestClass : BaseUnitTestWithValidator<TTestClass, TRequest, TResponse, TRequestHandler, TRequestValidator>
        where TRequest : IRequest<TResponse>
        where TRequestHandler : class, IRequestHandler<TRequest, TResponse>
        where TRequestValidator : class, IValidator<TRequest> {
        private Mock<TRequestValidator> _validator = default!;

        protected override void SetupMockRequestHandler(ValidationResult? expectedValidationResult = null) {
            _validator = new Mock<TRequestValidator>();
            _validator.Setup(validator => validator.ValidateAsync(_request))
                .ReturnsAsync(expectedValidationResult ?? new ValidationResult());

            _requestHandler = new Mock<TRequestHandler>(_dbContext.Object, _validator.Object);

            var requestHandlerSetup = _requestHandler
                .Setup(handler => handler.HandleAsync(_request));

            if (expectedValidationResult is not null) {
                requestHandlerSetup.ThrowsAsync(new ValidationException(expectedValidationResult.Errors));
            }
            else {
                requestHandlerSetup.ReturnsAsync(_expected);
            }
        }
    }
}
