using Microsoft.EntityFrameworkCore;
using qwikhr.Models;

namespace qwikhr.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureDefaultProperties(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var entity = builder.Entity(entityType.ClrType);

                // Set default GUID values
                foreach (var property in entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(Guid)))
                {
                    entity.Property(property.Name).HasDefaultValueSql("gen_random_uuid()");
                }

                // Set default CreatedAt timestamp
                var createdAtProperty = entityType.ClrType.GetProperty("CreatedAt");
                if (createdAtProperty != null)
                {
                    entity.Property(createdAtProperty.Name).HasDefaultValueSql("CURRENT_TIMESTAMP");
                }
            }

            builder.Entity<User>()
            .HasOne(u => u.Company)
            .WithMany()
            .HasForeignKey(u => u.CompanyId)  // Ensure foreign key mapping
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LeaveRequest>()
            .HasOne(lr => lr.Employee)
            .WithMany(e => e.LeaveRequests)
            .HasForeignKey(lr => lr.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }

}