using UnityEngine;


namespace Domination.Abstractions
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}
