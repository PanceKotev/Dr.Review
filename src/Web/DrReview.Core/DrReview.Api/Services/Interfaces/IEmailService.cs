namespace DrReview.Api.Services.Interfaces
{
    using DrReview.Contracts.Dtos.Emails;
    using FluentEmail.Core.Models;

    public interface IEmailService
    {
        public Task<SendResponse> SendEmailAsync(BaseEmailDto email);
    }
}
