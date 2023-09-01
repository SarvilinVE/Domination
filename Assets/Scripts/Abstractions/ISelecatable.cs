using UnityEngine;


namespace Domination.Abstractions
{
    public interface ISelecatable : IHealthHolder
    {
        Transform PivotPoint { get; }
        Sprite Icon { get; }
    }
}
