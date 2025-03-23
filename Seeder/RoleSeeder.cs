using Microsoft.AspNetCore.Identity;

namespace qwikhr.Seeder
{
    public static class RoleSeeder
    {
        public static IEnumerable<IdentityRole<int>> GetRoles()
        {
            return Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select((role, index) => new IdentityRole<int>
                {
                    Id = index + 1,
                    Name = role.ToString(),
                    NormalizedName = role.ToString().ToUpper()
                });
        }
    }
}