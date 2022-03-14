namespace DrReview.Common.Mediator.Contracts;

using MediatR;

public interface IPublishNotificationHandler<in T> : INotificationHandler<T>
    where T : INotification
{
}
