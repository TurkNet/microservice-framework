using MediatR;
using Noctools.Domain;

namespace Noctools.Application.CleanArch
{
    public interface IEventHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        IQueryRepositoryFactory QueryFactory { get; }
    }

    public interface ITxEventHandler<in TRequest, TResponse> : IEventHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        IUnitOfWorkAsync CommandFactory { get; }
    }
}