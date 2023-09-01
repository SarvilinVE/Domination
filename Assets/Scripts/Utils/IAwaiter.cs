using System.Runtime.CompilerServices;


namespace Domination.Utils
{
    public interface IAwaiter<TAwaited> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TAwaited GetResult();
    }
}
