using FluentAssertions;
using FluentValidation;
using Transparity.Application.Examples;
using Transparity.Shared.Models;
using Transparity.Tests.Unit.Abstractions;

namespace Transparity.Tests.Unit.Application.Examples {
    public class MediatorQueryTests : BaseUnitTest<MediatorQueryTests,
        ExampleMediatorQuery, Result<object>, ExampleMediatorQueryHandler> {
        protected override ExampleMediatorQueryHandler CreateRequestHandler() {
            return new();
        }


        [Fact]
        public void MediatorQuery_Runs_Successfully() {
            Arrange(
                request => { },
                expected => {
                    expected.Successful = true;
                    expected.Data = new {
                        Message = "Successful execution!"
                    };
                    expected.Errors = null;
                }
            )
            .Act()
            .Assert();
        }
    }

    public class MediatorCommandTests : BaseUnitTest<MediatorCommandTests,
        ExampleMediatorCommand, Result<object>, ExampleMediatorCommandHandler> {
        protected override ExampleMediatorCommandHandler CreateRequestHandler() {
            return new();
        }

        [Fact]
        public void MediatorCommand_Runs_Successfully() {
            Arrange(
                request => { },
                expected => {
                    expected.Successful = true;
                    expected.Data = new {
                        Message = "Successful execution!"
                    };
                    expected.Errors = null;
                }
            )
            .Act()
            .Assert();
        }
    }

    public class MediatorCommandWithValidatorTests : BaseUnitTestWithValidator<MediatorCommandWithValidatorTests,
       ExampleMediatorCommandWithValidator, Result<object>, ExampleMediatorCommandWithValidatorHandler,
       ExampleMediatorCommandWithValidatorValidator> {
        protected override ExampleMediatorCommandWithValidatorHandler CreateRequestHandler() {
            return new(_validator.Object);
        }

        [Fact]
        public void MediatorCommandWithValidator_Runs_Successfully() {
            Arrange(
                request => {
                    request.Id = 1;
                },
                expected => {
                    expected.Successful = true;
                    expected.Data = new {
                        Message = "Successful execution!"
                    };
                    expected.Errors = null;
                }
            )
            .Act()
            .Assert();
        }

        [Fact]
        public void ExampleMediatorCommandWithValidator_Validate_Required_Param_Throws_ValidationException() {
            Arrange(
                request => {
                    request.Id = 0;
                },
                validationResult => {
                    validationResult.Errors.Add(new("Id", "Id should be greater than 0"));
                }
            )
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);
            });
        }
    }
}
