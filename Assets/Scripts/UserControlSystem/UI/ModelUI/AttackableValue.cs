using Domination.Abstractions;
using UnityEngine;


namespace Domination.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy Game/" +
    nameof(AttackableValue), order = 0)]
    public class AttackableValue : ScriptableObjectValueBase<IAttackable>
    {
        //#region Fields

        //public event Action<IAttackable> OnNewValue;

        //#endregion


        //#region Properties

        //public IAttackable CurrentValue { get; private set; }

        //#endregion


        //#region Methods

        //public void SetValue(IAttackable value)
        //{
        //    CurrentValue = value;
        //    OnNewValue?.Invoke(value);
        //}

        //#endregion

    }
}
