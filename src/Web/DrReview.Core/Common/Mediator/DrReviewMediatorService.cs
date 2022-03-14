namespace DrReview.Common.Mediator;

using System.Threading.Tasks;
using DrReview.Common.Mediator.Contracts;
using DrReview.Common.Mediator.Interfaces;
using MediatR;

public class DrReviewMediatorService : IDrReviewMediatorService
{
    private readonly IMediator _mediator;

    public DrReviewMediatorService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishAsync(object notification)
    {
        await PublishAsync(notification, default);
    }

    public async Task PublishAsync<TNotification>(TNotification notification)
        where TNotification : IPublishNotification
    {
        await PublishAsync(notification, default);
    }

    public async Task PublishAsync<TNotification>(string jobName, TNotification notification)
        where TNotification : IPublishNotification
    {
        await PublishAsync(notification, default);
    }

    public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken)
        where TNotification : IPublishNotification
    {
        await _mediator.Publish(notification, cancellationToken);
    }

    public async Task PublishAsync(object notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(notification, cancellationToken);
    }

    public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> request)
    {
        return await SendAsync(request, default);
    }

    public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> request)
    {
        return await SendAsync(request, default);
    }

    public async Task<object?> SendAsync(object request)
    {
        return await SendAsync(request, default);
    }

    public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    public async Task<object?> SendAsync(object request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }
}
