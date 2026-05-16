using Transparity.Data.Abstractions;
using Transparity.Shared.Exceptions;

namespace Transparity.Data.Entities {
    public class User : IId, ISoftDelete {
        public long Id { get; }
        public Guid AuthId { get; private set; }
        public long UserInfoId { get; private set; }
        public long RoleId { get; private set; }
        public bool IsVerified { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DeletedAt { get; set; }

        public virtual UserInfo Info { get; private set; } = default!;
        public virtual Role Role { get; private set; } = default!;
        public virtual IEnumerable<Request> Requests { get; private set; } = default!;
        public virtual IEnumerable<RequestComment> Comments { get; private set; } = default!;
        public virtual IEnumerable<RequestAttachment> Attachments { get; private set; } = default!;

        public static User Create(Guid authId, UserInfo info, Role role, DateTime? utcNow = null) {
            DataException.ThrowIfDefault(authId, nameof(authId));
            DataException.ThrowIfNull(info, nameof(info));
            DataException.ThrowIfNull(role, nameof(role));

            return new() {
                AuthId = authId,
                Info = info,
                Role = role,
                CreatedAt = utcNow ?? DateTime.UtcNow
            };
        }
    }
}
   