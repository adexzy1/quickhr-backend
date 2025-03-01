using qwikhr.Models;

namespace qwikhr.Data;

using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Position> Positions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityByDefaultColumns();
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var guidProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(Guid));

            foreach (var property in guidProperties)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasDefaultValueSql("gen_random_uuid()");
            }

            var createdAtProperty = entityType.ClrType.GetProperties()
                .FirstOrDefault(p => p.Name == "CreatedAt");
            if (createdAtProperty != null)
                modelBuilder.Entity(entityType.ClrType)
                    .Property(createdAtProperty.Name)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
        base.OnModelCreating(modelBuilder);
    }


}