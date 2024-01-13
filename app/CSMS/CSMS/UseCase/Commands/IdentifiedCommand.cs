using MediatR;

namespace CSMS.UseCase.Commands
{
    public class IdentifiedCommand<T> : IRequest<T>
    {
        public T Command { get; }
        public Guid Id { get; }
        public IdentifiedCommand(T command)
        {
            Command = command;
        }
    }
}
