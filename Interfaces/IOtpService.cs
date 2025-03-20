namespace qwikhr.Interfaces
{
    public interface IOtpService
    {
        Task<string> GenerateOtp(int userId, OtpPurpose purpose);
        Task<bool> ValidateOtp(int userId, string otp, OtpPurpose purpose);
    }
}