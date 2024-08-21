using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.DAL.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleMappingEntity> UserRoleMappings { get; set; }
    public DbSet<VocationRequestEntity> VocationRequests { get; set; }
    public DbSet<LinkedUserEntity> LinkedUsers { get; set; }
    public DbSet<UserApprovalResponseEntity> UserApprovalResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleMappingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LinkedUserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VocationRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserApprovalResponseEntityTypeConfiguration());
    }
}
