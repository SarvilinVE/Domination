using Domination.Abstractions;
using System;
using UnityEngine;


namespace Domination.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy Game/" +
nameof(SelectableValue), order = 0)]
    public class SelectableValue : ScriptableObject
    {

        #region Fields

        public event Action<ISelecatable> OnSelected;

        #endregion


        #region Properties

        public ISelecatable CurrentValue { get; private set; }

        #endregion


        #region Methods

        public void SetValue(ISelecatable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }

        #endregion

    }
}
