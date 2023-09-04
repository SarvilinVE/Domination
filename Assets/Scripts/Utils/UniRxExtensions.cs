using System;
using UniRx;


namespace Domination.Utils
{
    public static class UniRxExtensions
    {

        #region Fields

        public static IDisposable Subscribe<T1, T2>(this IObservable<ValueTuple<T1, T2>> source, Action<T1, T2> onNext)
        {
            return ObservableExtensions.Subscribe(source, t => onNext(t.Item1, t.Item2));
        }

        #endregion

    }
}
