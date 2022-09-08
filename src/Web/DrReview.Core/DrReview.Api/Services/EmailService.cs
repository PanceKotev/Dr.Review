namespace DrReview.Api.Services
{
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.Dtos.Emails;
    using FluentEmail.Core;
    using FluentEmail.Core.Models;

    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<SendResponse> SendEmailAsync(BaseEmailDto email)
        {

            SendResponse result = await _fluentEmail
                        .To(email.Recipient)
                        .Subject(email.Subject)
                        .UsingTemplateFromFile($"publish/Emails/Views/{email.GetType().Name}.cshtml", email)
                        .Tag(email.GetType().Name)
                        .SendAsync();

            return result;
        }
    }
}
