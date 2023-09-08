using System.Threading.Tasks;

namespace Domination.Abstractions
{
    public interface ICommandExecutor
    {
        Task TryExecuteCommand(object command);
    }

    public interface ICommandExecutor<T> : ICommandExecutor where T : ICommand
    {

    }
}
