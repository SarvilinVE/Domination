using System;


namespace Domination.Utils
{
    public abstract class AwaiterBase<TAwaited> : IAwaiter<TAwaited>
    {

        #region Fields

        public Action _continuation;
        public bool _isCompleted;
        private TAwaited _result;

        #endregion


        #region Properties

        public bool IsCompleted => _isCompleted;

        #endregion


        #region Methods

        public TAwaited GetResult() => _result;

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                _continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }

        protected void OnWaitFinish(TAwaited result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }

        #endregion

    }
}
