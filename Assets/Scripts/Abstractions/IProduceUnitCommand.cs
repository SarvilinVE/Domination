using UnityEngine;


namespace Domination.Abstractions
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }
}
