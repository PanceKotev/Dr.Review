namespace DrReview.Contracts.Dtos.Emails
{
    public abstract class BaseEmailDto
    {
        protected BaseEmailDto(string recipient, string subject)
        {
            Recipient = recipient;
            Subject = subject;
        }

        public string Recipient { get; init; }

        public string Subject { get; init; }
    }
}
