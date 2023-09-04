using System;
using UniRx;


namespace Domination.Utils
{
    public abstract class StatelessScriptableObjectValueBase<T> : ScriptableObjectValueBase<T>, IObservable<T>
    {
        #region Fields

        private Subject<T> _innerDataSource = new Subject<T>();

        #endregion


        #region Methods

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _innerDataSource.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer) => _innerDataSource.Subscribe(observer);

        #endregion
    }
}
