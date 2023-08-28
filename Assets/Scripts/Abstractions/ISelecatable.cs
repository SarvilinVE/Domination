using UnityEngine;


namespace Domination.Abstractions
{
    public interface ISelecatable
    {
        float Health { get; }
        float MaxHeath { get; }
        Sprite Icon { get; }
    }
}
