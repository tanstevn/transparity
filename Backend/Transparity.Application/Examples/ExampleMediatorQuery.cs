using Transparity.Application.Abstractions;
using Transparity.Shared.Models;

namespace Transparity.Application.Examples {
    public class ExampleMediatorQuery : IQuery<Result<object>> { }

    public class ExampleMediatorQueryHandler : IRequestHandler<ExampleMediatorQuery, Result<object>> {
        public async Task<Result<object>> HandleAsync(ExampleMediatorQuery request) {
            return Result<object>
                .Success(new {
                    Message = "Successful execution!"
                });
        }
    }
}
