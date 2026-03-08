namespace Transparity.Application.Abstractions {
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
