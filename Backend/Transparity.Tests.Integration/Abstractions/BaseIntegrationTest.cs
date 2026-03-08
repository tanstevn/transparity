using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transparity.Application.Abstractions;
using Transparity.Data;
using Transparity.Infrastructure.Mediator;
using Transparity.Tests.Integration.Fixtures;

namespace Transparity.Tests.Integration.Abstractions {
    [Collection("Postgres")]
    public abstract class BaseIntegrationTest<TTestClass, TRequest,
        TResponse, TRequestHandler> : IAsyncLifetime
        where TTestClass : BaseIntegrationTest<TTestClass, TRequest, TResponse, TRequestHandler>
        where TRequest : IRequest<TResponse>
        where TRequestHandler : class, IRequestHandler<TRequest, TResponse> {
        private readonly PostgresFixture _fixture;

        private ServiceProvider _provider = default!;
        private ApplicationDbContext _dbContext = default!;
        private IMediator _mediator = default!;

        private TRequest _request = default!;
        private TResponse _result = default!;
        private Exception _exception = default!;

        protected BaseIntegrationTest(PostgresFixture fixture) {
            _fixture = fixture;
        }

        public async Task InitializeAsync() {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_fixture.ConnectionString));

            services.AddMediatorFromAssembly(typeof(IMediator).Assembly);
            services.AddValidatorsFromAssembly(typeof(IMediator).Assembly);

            _provider = services.BuildServiceProvider();

            _dbContext = _provider.GetRequiredService<ApplicationDbContext>();
            _mediator = _provider.GetRequiredService<IMediator>();
        }

        public TTestClass Arrange(Action<TRequest> arrange) {
            _request = Activator.CreateInstance<TRequest>();
            arrange(_request);

            return (TTestClass)this;
        }

        public TTestClass Act() {
            try {
                
                _result = _mediator.SendAsync(_request)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception ex) {
                _exception = ex;
            }

            return (TTestClass)this;
        }

        public void Assert(Action<TResponse>? assertion = null) {
            _result.Should()
                .NotBeNull();

            if (assertion is not null) {
                assertion(_result);
            }
        }

        public void AssertThrows<TException>(Action<TException> assertion)
            where TException : Exception {
            ArgumentNullException.ThrowIfNull(_exception);

            _exception.Should()
                .BeOfType<TException>();

            assertion((TException)_exception);
        }

        public async Task DisposeAsync() {
            await _provider.DisposeAsync();
        }
    }
}
