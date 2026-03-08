namespace Transparity.Application.Abstractions {
    public interface IMediator {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}
