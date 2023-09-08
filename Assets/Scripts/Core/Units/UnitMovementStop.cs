using Domination.Abstractions;
using Domination.Utils;
using System;
using UniRx;
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
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private int _throttleFrames = 60;
        [SerializeField] private int _continuityTreshhold = 10;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _collisionDetector.Collisions
                .Where(_ => _agent.hasPath)
                .Where(collision => collision.collider.GetComponent<IUnit>() != null)
                .Select(_ => Time.frameCount)
                .Distinct()
                .Buffer(_throttleFrames)
                .Where(buffer =>
                {
                    for (var i = 1; i< buffer.Count; i++)
                    {
                        if (buffer[i] - buffer[i-1] > _continuityTreshhold)
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .Subscribe(_ =>
                {
                    _agent.isStopped = true;
                    _agent.ResetPath();
                    onStop?.Invoke();
                })
                .AddTo(this);
        }

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
