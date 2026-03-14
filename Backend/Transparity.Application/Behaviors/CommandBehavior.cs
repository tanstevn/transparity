using Transparity.Application.Abstractions;
using Transparity.Data;
using Transparity.Shared.Attributes;

namespace Transparity.Application.Behaviors {
    [PipelineOrder(2)]
    internal class CommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse> {
        private readonly ApplicationDbContext _dbContext;

        public CommandBehavior(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next) {
            using var transaction = _dbContext.Database
                .BeginTransaction();

            try {
                var result = await next();
                await transaction.CommitAsync();

                return result;
            }
            catch {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
