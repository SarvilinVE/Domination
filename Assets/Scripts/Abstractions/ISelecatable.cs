using UnityEngine;


namespace Domination.Abstractions
{
    public interface ISelecatable : IHealthHolder, IIconHolder
    {
        Transform PivotPoint { get; }
    }
}
