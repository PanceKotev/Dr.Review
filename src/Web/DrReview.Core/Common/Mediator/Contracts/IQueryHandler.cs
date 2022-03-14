namespace DrReview.Common.Mediator.Contracts;

using MediatR;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
