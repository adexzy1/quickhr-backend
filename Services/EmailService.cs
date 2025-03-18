using FluentEmail.Core;
using qwikhr.Interfaces;
using qwikhr.Models;

namespace qwikhr.Services
{
    public class EmailService(IFluentEmail fluentEmail) : IEmailService
    {
        private readonly IFluentEmail _fluentEmail = fluentEmail;

        public async Task Send(EmailMetadata metadata)
        {
            await _fluentEmail.To(metadata.ToAddress)
            .Subject(metadata.Subject)
            .Body(metadata.Body)
            .SendAsync();
        }
    }
}