using System;
using UnityEngine;


namespace Domination.Utils
{
    public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>
    {

        public class NewValueNotifer<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;

            public NewValueNotifer(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
            {
                _scriptableObjectValueBase = scriptableObjectValueBase;
                _scriptableObjectValueBase.OnNewValue += OnNewValue;
            }

            private void OnNewValue(TAwaited obj)
            {
                _scriptableObjectValueBase.OnNewValue -= OnNewValue;
                OnWaitFinish(obj);
            }
        }


        #region Fields

        public event Action<T> OnNewValue;

        #endregion


        #region Properties

        public T CurrentValue { get; private set; }

        #endregion


        #region Methods

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifer<T>(this);
        }

        #endregion

    }
}
