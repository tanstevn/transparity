using Transparity.Data.Abstractions;
using Transparity.Data.Records;

namespace Transparity.Data.Entities {
    public class UserInfo : IId {
        public long Id { get; }
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string? Mobile { get; private set; }
        public string Address1 { get; private set; } = default!;
        public string? Address2 { get; private set; }

        public virtual User User { get; private set; } = default!;

        public static UserInfo Create(UserInfoRecord info) {
            return new() {
                FirstName = info.FirstName,
                LastName = info.LastName,
                Email = info.Email,
                Mobile = info.Mobile,
                Address1 = info.Address1,
                Address2 = info.Address2
            };
        }
    }
}
