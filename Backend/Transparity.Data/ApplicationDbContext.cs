using Microsoft.EntityFrameworkCore;
using Transparity.Data.Entities;

namespace Transparity.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : DbContext(options) {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestAttachment> RequestAttachments { get; set; }
        public DbSet<RequestCategory> RequestCategories { get; set; }
        public DbSet<RequestComment> RequestComments { get; set; }
        public DbSet<RequestField> RequestFields { get; set; }
        public DbSet<RequestHistory> RequestHistory { get; set; }
        public DbSet<RequestLevel> RequestLevels { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("core");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
