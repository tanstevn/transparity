using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Transparity.Application.Abstractions;
using Transparity.Shared.Attributes;

namespace Transparity.Infrastructure.Mediator {
    internal class Executor<TRequest, TResponse> : IExecutor 
        where TRequest : IRequest<TResponse> {
        public async Task<object> ExecuteAsync(object request, IServiceProvider provider) {
            var requestHandler = provider
                .GetRequiredService<IRequestHandler<TRequest, TResponse>>();

            RequestHandlerDelegate<TResponse> finalHandler =
                () => requestHandler.HandleAsync((TRequest)request);

            var behaviors = provider
                .GetServices<IPipelineBehavior<TRequest, TResponse>>();

            var behaviorsOrder = behaviors.OrderByDescending(
                behavior => behavior.GetType()
                    .GetCustomAttribute<PipelineOrderAttribute>()!
                    .Order);

            var aggregateResult =  behaviorsOrder.Aggregate(finalHandler, (next, behavior) => 
                () => behavior.HandleAsync((TRequest)request, next));

            return (await aggregateResult())!;
        }
    }
}
