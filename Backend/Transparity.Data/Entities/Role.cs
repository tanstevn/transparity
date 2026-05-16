using Transparity.Data.Abstractions;
using Transparity.Shared.Exceptions;

namespace Transparity.Data.Entities {
    public class Role : IId, ISoftDelete {
        public long Id { get; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? DeletedAt { get; set; }

        public virtual IEnumerable<User> Users { get; private set; } = default!;

        public static Role Create(string name, string description, DateTime? utcNow = null) {
            DataException.ThrowIfNullOrWhitespace(name, nameof(name));
            DataException.ThrowIfNullOrWhitespace(description, nameof(description));

            return new() {
                Name = name,
                Description = description,
                CreatedAt = utcNow ?? DateTime.UtcNow
            };
        }
    }
}
