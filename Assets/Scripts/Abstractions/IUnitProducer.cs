using UniRx;

namespace Domination.Abstractions
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        void Cancel(int index);
    }
}
