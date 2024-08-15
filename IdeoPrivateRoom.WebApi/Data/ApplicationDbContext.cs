using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.WebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
    public DbSet<Vocation> Vocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleMappingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VocationEntityTypeConfiguration());
    }
}
