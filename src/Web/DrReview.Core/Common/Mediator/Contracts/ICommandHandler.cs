namespace DrReview.Common.Mediator.Contracts;

using MediatR;

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
