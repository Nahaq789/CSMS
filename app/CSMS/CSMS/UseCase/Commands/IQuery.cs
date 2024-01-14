using MediatR;

namespace CSMS.UseCase.Commands;

public interface IQuery<TResponse> : IRequest<TResponse>
{

}