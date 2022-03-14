namespace DrReview.Common.Mediator.Contracts;

using MediatR;

public interface ICommand<out T> : IRequest<T>
{
}
