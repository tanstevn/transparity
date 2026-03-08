using Transparity.Application.Examples;
using Transparity.Shared.Models;
using Transparity.Tests.Unit.Abstractions;

namespace Transparity.Tests.Unit.Application.Examples {
    public class MediatorQueryTests : BaseUnitTest<MediatorQueryTests,
        ExampleMediatorQuery, Result<object>, ExampleMediatorQueryHandler> {
        protected override ExampleMediatorQueryHandler CreateRequestHandler() {
            return new();
        }


        [Fact]
        public void MediatorQuery_Runs_Successfully() {
            Arrange(
                request => { },
                expected => {
                    expected.Successful = true;
                    expected.Data = new {
                        Message = "Successful execution!"
                    };
                    expected.Errors = null;
                }
            )
            .Act()
            .Assert();
        }
    }
}
