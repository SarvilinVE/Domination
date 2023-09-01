using Domination.Abstractions;
using UnityEngine;


namespace Domination.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy Game/" +
nameof(SelectableValue), order = 0)]
    public class SelectableValue : ScriptableObjectValueBase<ISelecatable>
    {

        //#region Fields

        //public event Action<ISelecatable> OnNewValue;

        //#endregion


        //#region Properties

        //public ISelecatable CurrentValue { get; private set; }

        //#endregion


        //#region Methods

        //public void SetValue(ISelecatable value)
        //{
        //    CurrentValue = value;
        //    OnNewValue?.Invoke(value);
        //}

        //#endregion

    }
}
