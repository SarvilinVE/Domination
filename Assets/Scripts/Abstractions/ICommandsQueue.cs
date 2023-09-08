namespace Domination.Abstractions
{
    public interface ICommandsQueue
    {
        ICommand CurrentCommand { get; }
        void EnqueueCommand(object command);
        void Cancel();
    }
}
