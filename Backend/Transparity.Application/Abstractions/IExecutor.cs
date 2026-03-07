namespace Transparity.Application.Abstractions {
    public interface IExecutor {
        Task<object> ExecuteAsync(object request, IServiceProvider provider);
    }
}
