using FluentAssertions;
using FluentValidation;
using Transparity.Application.Examples;
using Transparity.Shared.Models;
using Transparity.Tests.Integration.Abstractions;
using Transparity.Tests.Integration.Fixtures;

namespace Transparity.Tests.Integration.Application.Examples {
    public class MediatorQueryTests : BaseIntegrationTest<MediatorQueryTests,
        ExampleMediatorQuery, Result<object>, ExampleMediatorQueryHandler> {
        public MediatorQueryTests(PostgresFixture fixture) : base(fixture) { }

        [Fact]
        public void ExampleMediatorQuery_Runs_Successfully() {
            Arrange()
            .Act()
            .Assert(result => {
                result.Successful
                    .Should()
                    .BeTrue();

                result.Data
                    .Should()
                    .NotBeNull();
            });
        }

        [Fact]
        public void ExampleMediatorQuery_NullReference_Throws_ArgumentNullException() {
            Arrange(setRequestToNull: true)
            .Act()
            .AssertThrows<ArgumentNullException>(ex => {
                ex.Message
                    .Should()
                    .Contain("Value cannot be null. (Parameter 'request')");
            });
        }
    }

    public class MediatorCommandTests : BaseIntegrationTest<MediatorCommandTests,
        ExampleMediatorCommand, Result<object>, ExampleMediatorCommandHandler> {
        public MediatorCommandTests(PostgresFixture fixture) : base(fixture) { }

        [Fact]
        public void ExampleMediatorCommand_Runs_Successfully() {
            Arrange()
            .Act()
            .Assert(result => {
                result.Successful
                    .Should()
                    .BeTrue();

                result.Data
                    .Should()
                    .NotBeNull();
            });
        }

        [Fact]
        public void ExampleMediatorCommand_NullReference_Throws_ArgumentNullException() {
            Arrange(setRequestToNull: true)
            .Act()
            .AssertThrows<ArgumentNullException>(ex => {
                ex.Message
                    .Should()
                    .Contain("Value cannot be null. (Parameter 'request')");
            });
        }
    }

    public class MediatorCommandWithValidatorTests : BaseIntegrationTest<MediatorCommandWithValidatorTests,
        ExampleMediatorCommandWithValidator, Result<object>, ExampleMediatorCommandWithValidatorHandler> {
        public MediatorCommandWithValidatorTests(PostgresFixture fixture) : base(fixture) { }

        [Fact]
        public void ExampleMediatorCommandWithValidator_Runs_Successfully() {
            Arrange(request => {
                request.Id = 1;
            })
            .Act()
            .Assert(result => {
                result.Successful
                    .Should()
                    .BeTrue();

                result.Data
                    .Should()
                    .NotBeNull();
            });
        }

        [Fact]
        public void ExampleMediatorCommandWithValidator_NullReference_Throws_ArgumentNullException() {
            Arrange(setRequestToNull: true)
            .Act()
            .AssertThrows<ArgumentNullException>(ex => {
                ex.Message
                    .Should()
                    .Contain("Value cannot be null. (Parameter 'request')");
            });
        }

        [Fact]
        public void ExampleMediatorCommandWithValidator_Validate_Required_Param_Throws_ValidationException() {
            Arrange(request => {
                request.Id = 0;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(error => error.PropertyName == "Id"
                        && error.ErrorMessage == "Id should be greater than 0");
            });
        }
    }
}
