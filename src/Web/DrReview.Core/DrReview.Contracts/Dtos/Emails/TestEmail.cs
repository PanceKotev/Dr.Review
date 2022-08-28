namespace DrReview.Contracts.Dtos.Emails
{
    public class TestEmail : BaseEmailDto
    {
        public TestEmail(string recipient, string subject, string testProperty)
            : base(recipient, subject)
        {
            TestProperty = testProperty;
        }

        public string TestProperty { get; init; }
    }
}
