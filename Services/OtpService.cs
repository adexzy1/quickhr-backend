using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Services
{
    public class OtpService(ApplicationDbContext dbContext) : IOtpService
    {
        private readonly ApplicationDbContext _context = dbContext;
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public async Task<string> GenerateOtp(int userId, OtpPurpose purpose)
        {
            string otp = GenerateOtpCode();
            var otpRecord = new Otp
            {
                UserId = userId,
                Purpose = purpose,
                Code = otp,
                IsUsed = false,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5)
            };
            _context.Otps.Add(otpRecord);
            await _context.SaveChangesAsync();
            return otp;
        }

        public async Task<bool> ValidateOtp(int userId, string otp, OtpPurpose purpose)
        {
            var otpRecord = await _context.Otps.FirstOrDefaultAsync(o => o.UserId == userId && o.Code == otp && o.Purpose == purpose && o.ExpiryTime > DateTime.UtcNow && o.IsUsed != true);

            if (otpRecord != null)
            {
                otpRecord.IsUsed = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private static string GenerateOtpCode(int length = 4)
        {
            var bytes = new byte[length];
            _rng.GetBytes(bytes);
            var otp = new string(Array.ConvertAll(bytes, b => (char)('0' + (b % 10))));
            return otp;
        }

    }
}