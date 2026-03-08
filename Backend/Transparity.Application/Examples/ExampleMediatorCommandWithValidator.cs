using FluentValidation;
using Transparity.Application.Abstractions;
using Transparity.Shared.Models;

namespace Transparity.Application.Examples {
    public class ExampleMediatorCommandWithValidator : ICommand<Result<object>> {
        public long Id { get; set; }
    }

    public class ExampleMediatorCommandWithValidatorValidator 
        : AbstractValidator<ExampleMediatorCommandWithValidator> {
        public ExampleMediatorCommandWithValidatorValidator() {
            RuleFor(param => param.Id)
                .GreaterThan(0)
                .WithMessage("Id should be greater than 0");
        }
    }

    public class ExampleMediatorCommandWithValidatorHandler 
        : IRequestHandler<ExampleMediatorCommandWithValidator, Result<object>> {
        private readonly IValidator<ExampleMediatorCommandWithValidator> _validator;

        public ExampleMediatorCommandWithValidatorHandler(
            IValidator<ExampleMediatorCommandWithValidator> validator) {
            _validator = validator;
        }

        public async Task<Result<object>> HandleAsync(ExampleMediatorCommandWithValidator request) {
            ArgumentNullException.ThrowIfNull(request);
            await _validator.ValidateAndThrowAsync(request);

            return Result<object>
                .Success(new {
                    Message = "Successful execution!"
                });
        }
    }
}
