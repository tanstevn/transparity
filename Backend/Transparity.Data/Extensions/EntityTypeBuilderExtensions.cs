using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transparity.Data.Abstractions;

namespace Transparity.Data.Extensions {
    internal static class EntityTypeBuilderExtensions {
        public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class {
            builder.HasKey("Id");
            builder.Property("Id")
                .ValueGeneratedOnAdd();

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class {
            if (!typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity))) {
                return builder;
            }

            builder.HasIndex("DeletedAt");
            builder.HasQueryFilter(entity => !((ISoftDelete)entity).DeletedAt.HasValue);

            return builder;
        }
    }
}
