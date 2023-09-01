using UnityEngine;

namespace Domination.Abstractions
{
    public interface IPatrolCommand : ICommand
    {
        Vector3 From { get; }
        Vector3 To { get; }
    }
}