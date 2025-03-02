using qwikhr.Models;

namespace qwikhr.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Position> Positions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseIdentityByDefaultColumns();
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var guidProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(Guid));

            foreach (var property in guidProperties)
            {
                builder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasDefaultValueSql("gen_random_uuid()");
            }

            var createdAtProperty = entityType.ClrType.GetProperties()
                .FirstOrDefault(p => p.Name == "CreatedAt");
            if (createdAtProperty != null)
                builder.Entity(entityType.ClrType)
                    .Property(createdAtProperty.Name)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

        }

        var roles = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select((role, index) => new IdentityRole<int>
            {
                Id = index + 1,
                Name = role.ToString(),
                NormalizedName = role.ToString().ToUpper()
            });

        builder.Entity<IdentityRole<int>>().HasData(roles);
    }


}