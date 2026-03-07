using FluentAssertions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Transparity.Application.Abstractions;
using Transparity.Data;

namespace Transparity.Tests.Unit.Abstractions {
    internal abstract class BaseUnitTest<TTestClass, TRequest, TResponse, TRequestHandler>
        where TTestClass: BaseUnitTest<TTestClass, TRequest, TResponse, TRequestHandler>
        where TRequest : IRequest<TResponse>
        where TRequestHandler : class, IRequestHandler<TRequest, TResponse> {
        protected Mock<ApplicationDbContext> _dbContext = default!;
        protected Mock<TRequestHandler> _requestHandler = default!;
        protected TRequest _request = default!;
        protected TResponse _expected = default!;
        private TResponse _result = default!;
        private Exception _exception = default!;

        protected BaseUnitTest() {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Neon")
                .Options;

            _dbContext = new Mock<ApplicationDbContext>(dbContextOptions);
        }

        protected virtual void SetupMockRequestHandler(ValidationResult? expectedValidationResult = null) {
            _requestHandler = new Mock<TRequestHandler>(_dbContext.Object);
            _requestHandler.Setup(handler => handler.HandleAsync(_request))
                .ReturnsAsync(_expected);
        }

        public TTestClass Arrange(Action<TRequest> arrangeRequest, Action<TResponse> arrangeExpected) {
            _request = Activator.CreateInstance<TRequest>();
            arrangeRequest(_request);

            _expected = Activator.CreateInstance<TResponse>();
            arrangeExpected(_expected);

            SetupMockRequestHandler();
            return (TTestClass)this;
        }

        public TTestClass Arrange(Action<TRequest> arrangeRequest, Action<ValidationResult> arrangeValidationResult) {
            _request = Activator.CreateInstance<TRequest>();
            arrangeRequest(_request);

            var expectedValidationResult = new ValidationResult();
            arrangeValidationResult(expectedValidationResult);

            SetupMockRequestHandler(expectedValidationResult);
            return (TTestClass)this;
        }

        public TTestClass Act() {
            try {
                _result = _requestHandler.Object
                    .HandleAsync(_request)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception ex) {
                _exception = ex;
            }

            return (TTestClass)this;
        }

        public void Assert(Action<TResponse>? assertion = null) {
            _result.Should()
                .Be(_expected);

            if (assertion is not null) {
                assertion(_result);
            }
        }

        public void AssertThrows<TException>(Action<TException>? assertion = null)
            where TException : Exception {
            _exception.Should()
                .NotBeNull();

            _exception.Should()
                .BeOfType<TException>();

            if (assertion is not null) {
                assertion((TException)_exception);
            }
        }
    }
}
