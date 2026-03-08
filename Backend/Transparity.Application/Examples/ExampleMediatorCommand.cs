using Transparity.Application.Abstractions;
using Transparity.Shared.Models;

namespace Transparity.Application.Examples {
    public class ExampleMediatorCommand : ICommand<Result<object>> { }

    public class ExampleMediatorCommandHandler : IRequestHandler<ExampleMediatorCommand, Result<object>> {
        public async Task<Result<object>> HandleAsync(ExampleMediatorCommand request) {
            return Result<object>
                .Success(new {
                    Message = "Successful execution!"
                });
        }
    }
}
