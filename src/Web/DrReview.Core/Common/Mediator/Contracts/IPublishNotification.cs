namespace DrReview.Common.Mediator.Contracts;

using System;
using MediatR;

public interface IPublishNotification : INotification
{
    public DateTime ProcessedOn { get; set; }

    public long? CreatedById { get; set; }
}
