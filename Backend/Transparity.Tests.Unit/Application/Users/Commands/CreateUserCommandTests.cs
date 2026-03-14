using FluentAssertions;
using FluentValidation;
using Transparity.Application.Users.Commands;
using Transparity.Data.Records;
using Transparity.Shared.Enums;
using Transparity.Shared.Models;
using Transparity.Tests.Unit.Abstractions;

namespace Transparity.Tests.Unit.Application.Users.Commands {
    public class CreateUserCommandTests : BaseUnitTestWithValidator<CreateUserCommandTests,
        CreateUserCommand, Result<CreateUserCommandResult>, CreateUserCommandHandler, CreateUserCommandValidator> {
        protected override CreateUserCommandHandler CreateRequestHandler() {
            return new(_dbContext.Object);
        }

        [Fact]
        public void CreateUserCommand_Runs_Successfully() {
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: "Test FirstName",
                    LastName: "Test LastName",
                    Email: "test@test.com",
                    Address1: "Test Address 1");
                request.RoleId = RoleEnums.Citizen;
            })
            .Act()
            .Assert(result => {
                result.Successful
                    .Should()
                    .BeTrue();

                result.Errors
                    .Should()
                    .BeNull();

                result.Data
                    .Should()
                    .NotBeNull();
            });
        }

        [Fact]
        public void CreateUserCommand_UserId_Param_Validation_Fails() {
            Arrange(request => {
                request.UserId = Guid.Empty;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "UserId"
                        && err.ErrorMessage == "User id must be a valid GUID value and must not be empty");
            });
        }

        [Fact]
        public void CreateUserCommand_RoleId_Param_Validation_Fails() {
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: "Test FirstName",
                    LastName: "Test LastName",
                    Email: "test@test.com",
                    Address1: "Test Address 1");
                request.RoleId = RoleEnums.None;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "RoleId"
                        && err.ErrorMessage == "Role id must be well-specified with the right value");
            });
        }

        [Fact]
        public void CreateUserCommand_Info_Param_Validation_Fails() {
            #region First name fail test
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: string.Empty,
                    LastName: "Test LastName",
                    Email: "test@test.com",
                    Address1: "Test Address 1");
                request.RoleId = RoleEnums.Citizen;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "Info.FirstName"
                        && err.ErrorMessage == "First name must not be empty");
            });
            #endregion

            #region Last name fail test
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: "Test FirstName",
                    LastName: string.Empty,
                    Email: "test@test.com",
                    Address1: "Test Address 1");
                request.RoleId = RoleEnums.Citizen;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "Info.LastName"
                        && err.ErrorMessage == "Last name must not be empty");
            });
            #endregion

            #region Email being empty fail test
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: "Test FirstName",
                    LastName: "Test LastName",
                    Email: string.Empty,
                    Address1: "Test Address 1");
                request.RoleId = RoleEnums.Citizen;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "Info.Email"
                        && err.ErrorMessage == "Email must not be empty");
            });
            #endregion

            #region Email being invalid email address format fail test
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: "Test FirstName",
                    LastName: "Test LastName",
                    Email: "test.com",
                    Address1: "Test Address 1");
                request.RoleId = RoleEnums.Citizen;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "Info.Email"
                        && err.ErrorMessage == "Email must be a valid email address");
            });
            #endregion

            #region Mobile number fail test
            Arrange(request => {
                request.UserId = Guid.NewGuid();
                request.Info = new UserInfoRecord(
                    FirstName: "Test FirstName",
                    LastName: "Test LastName",
                    Email: "test@test.com",
                    Address1: "Test Address 1",
                    Mobile: "123456789");
                request.RoleId = RoleEnums.Citizen;
            })
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors
                    .Should()
                    .HaveCountGreaterThan(0);

                ex.Errors
                    .Should()
                    .Contain(err => err.PropertyName == "Info.Mobile"
                        && err.ErrorMessage == "Mobile number must be 11 digits");
            });
            #endregion
        }
    }
}
