using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transparity.Data.Abstractions;

namespace Transparity.Data.Extensions {
    internal static class EntityTypeBuilderExtensions {
        public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IId {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id)
                .ValueGeneratedOnAdd();

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, ISoftDelete {
            builder.HasIndex(entity => entity.DeletedAt);
            builder.HasQueryFilter(entity => !entity.DeletedAt.HasValue);

            return builder;
        }
    }
}
