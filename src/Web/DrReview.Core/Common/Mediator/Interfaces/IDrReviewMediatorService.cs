namespace DrReview.Common.Mediator.Interfaces;

using System.Threading.Tasks;
using DrReview.Common.Mediator.Contracts;

public interface IDrReviewMediatorService
{
    Task PublishAsync(object notification);

    Task PublishAsync<TNotification>(TNotification notification)
        where TNotification : IPublishNotification;

    Task PublishAsync<TNotification>(string jobName, TNotification notification)
        where TNotification : IPublishNotification;

    Task PublishAsync(object notification, CancellationToken cancellationToken);

    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken)
        where TNotification : IPublishNotification;

    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> request);

    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> request);

    Task<object?> SendAsync(object request);

    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken);

    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken);

    Task<object?> SendAsync(object request, CancellationToken cancellationToken);
}
