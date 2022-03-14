namespace DrReview.Common.Mediator.Contracts;

using System;

public abstract class DomainEvent : IPublishNotification
{
    protected DomainEvent(long? createdById)
    {
        ProcessedOn = DateTime.UtcNow;
        CreatedById = createdById;
    }

    public DateTime ProcessedOn { get; set; }

    public long? CreatedById { get; set; }
}
