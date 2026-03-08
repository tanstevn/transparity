using Transparity.Application.Abstractions;
using Transparity.Shared.Attributes;

namespace Transparity.Application.Examples {
    [PipelineOrder(1)]
    public class ExamplePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse> {
        public async Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next) {
            ArgumentNullException.ThrowIfNull(request);
            return await next();
        }
    }
}
