using UnityEngine;


namespace Domination.UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy Game/"
+ nameof(Vector3Value), order = 0)]
    public class Vector3Value : ScriptableObjectValueBase<Vector3>
    {

        //#region Fields

        //public event Action<Vector3> onNewValue;

        //#endregion


        //#region Properties

        //public Vector3 CurrentValue { get; private set; }

        //#endregion


        //#region Methods

        //public void SetValue(Vector3 value)
        //{
        //    CurrentValue = value;
        //    onNewValue?.Invoke(value);
        //}

        //#endregion

    }
}
