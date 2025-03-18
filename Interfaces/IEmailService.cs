
using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface IEmailService
    {
        Task Send(EmailMetadata metadata);
    }
}