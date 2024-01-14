using MediatR;

namespace CSMS.UseCase.Commands;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{

}
