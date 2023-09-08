using System;


namespace Domination.Abstractions
{
    public interface IGameStatus
    {
        IObservable<int> Status { get; }
    }
}
