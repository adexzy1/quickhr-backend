using qwikhr.Dtos.Company;
namespace qwikhr.Dtos.Auth
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public Guid? Slug { get; set; }
        public string? Email { get; set; }
        public CompanyDto? Company { get; set; }
        public bool? EmailVerified { get; set; }
        public string? Token { get; set; }
        public List<string>? Roles { get; set; }

    }
}