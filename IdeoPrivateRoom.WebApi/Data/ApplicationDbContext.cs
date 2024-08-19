using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.WebApi.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
    public DbSet<VocationRequest> VocationRequests { get; set; }
    public DbSet<LinkedUser> LinkedUsers { get; set; }
    public DbSet<UserApprovalResponse> UserApprovalResponses { get; set; }

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
