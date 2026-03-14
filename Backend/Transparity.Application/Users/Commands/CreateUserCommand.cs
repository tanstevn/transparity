using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Transparity.Application.Abstractions;
using Transparity.Data;
using Transparity.Data.Entities;
using Transparity.Data.Records;
using Transparity.Shared.Enums;
using Transparity.Shared.Models;

namespace Transparity.Application.Users.Commands {
    public class CreateUserCommand : ICommand<Result<CreateUserCommandResult>>, IUserScoped {
        public Guid UserId { get; set; }
        public UserInfoRecord Info { get; set; } = default!;
        public RoleEnums RoleId { get; set; }
    }

    public class CreateUserCommandResult {
        public string Message { get; set; } = default!;
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
        public CreateUserCommandValidator() {
            RuleFor(param => param.UserId)
                .NotEmpty()
                .WithMessage("User id must be a valid GUID value and must not be empty");

            RuleFor(param => param.Info)
                .NotEmpty()
                .WithMessage("User information must be well-specified with the right data")
                .SetValidator(new UserInfoRecordValidator());

            RuleFor(param => param.RoleId)
                .NotEmpty()
                .WithMessage("Role id must be well-specified with the right value");
        }
    }

    public class UserInfoRecordValidator : AbstractValidator<UserInfoRecord> {
        public UserInfoRecordValidator() {
            RuleFor(param => param.FirstName)
                .NotEmpty()
                .WithMessage("First name must not be empty");

            RuleFor(param => param.LastName)
                .NotEmpty()
                .WithMessage("Last name must not be empty");

            RuleFor(param => param.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty")
                .EmailAddress()
                .WithMessage("Email must be a valid email address");

            RuleFor(param => param.Mobile)
                .Matches(@"^\d{11}$")
                .WithMessage("Mobile number must be 11 digits")
                .When(param => !string.IsNullOrWhiteSpace(param.Mobile));
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResult>> {
        private readonly ApplicationDbContext _dbContext;

        public CreateUserCommandHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<CreateUserCommandResult>> HandleAsync(CreateUserCommand request) {
            var role = await _dbContext.Roles
                .FindAsync((long)request.RoleId);;

            if (role is null) {
                throw new ValidationException("Role id does not exist");
            }

            var userInfo = UserInfo.Create(request.Info);
            var user = User.Create(request.UserId, userInfo, role);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Result<CreateUserCommandResult>
                .Success(new() {
                    Message = "User created successfully"
                });
        }
    }
}
