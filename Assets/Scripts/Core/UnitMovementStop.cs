using Domination.Utils;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Domination.Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {

        #region Class

        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {

            #region Fields

            private readonly UnitMovementStop _unitMovementStop;

            #endregion


            #region Methods

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.onStop += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.onStop -= OnStop;
                OnWaitFinish(new AsyncExtensions.Void());
            }

            #endregion

        }

        #endregion


        #region Fields

        public Action onStop;

        [SerializeField] private NavMeshAgent _agent;

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0.0f)
                    {
                        onStop?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        #endregion

    }
}
