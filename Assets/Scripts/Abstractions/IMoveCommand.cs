using UnityEngine;

namespace Domination.Abstractions
{
    public interface IMoveCommand : ICommand
    {
        Vector3 Target { get; }
    }
}