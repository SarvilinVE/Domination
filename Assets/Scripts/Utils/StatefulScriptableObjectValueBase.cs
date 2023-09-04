using System;
using UniRx;


namespace Domination.Utils
{
    public abstract class StatefulScriptableObjectValueBase<T> : ScriptableObjectValueBase<T>, IObservable<T>
    {

        #region Fields

        private ReactiveProperty<T> _innerDataSource = new ReactiveProperty<T>();

        #endregion


        #region Methods

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _innerDataSource.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer) => _innerDataSource.Subscribe(observer);

        #endregion

    }
}
