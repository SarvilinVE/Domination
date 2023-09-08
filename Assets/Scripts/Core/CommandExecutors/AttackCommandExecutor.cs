using Domination.Abstractions;
using Domination.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Domination.Core.CommandExecutor
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {

        #region Class AttackOperation

        public class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {

            #region Class AttackOperationAwaiter

            public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {

                #region Fields

                private AttackOperation _attackOperation;

                #endregion


                #region Methods

                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    _attackOperation.OnComplete += OnComplete;
                }

                private void OnComplete()
                {
                    _attackOperation.OnComplete -= OnComplete;
                    OnWaitFinish(new AsyncExtensions.Void());
                }

                #endregion

            }

            #endregion


            #region Fields

            private event Action OnComplete;

            private readonly AttackCommandExecutor _attackCommandExecutor;
            private readonly IAttackable _target;

            private bool _isCancelled;

            #endregion


            #region ClassLifeCycle

            public AttackOperation(AttackCommandExecutor attackCommand, IAttackable target)
            {
                _attackCommandExecutor = attackCommand;
                _target = target;

                var thread = new Thread(AttackAlgorythm);
                thread.Start();
            }

            #endregion


            #region Methods

            public void Cancel()
            {
                _isCancelled = true;
                OnComplete?.Invoke();
            }

            private void AttackAlgorythm()
            {
                while (true)
                {
                    if (_attackCommandExecutor == null
                        || _attackCommandExecutor._ourHealth.Health == 0
                        || _target.Health == 0
                        || _isCancelled == true)
                    {
                        OnComplete?.Invoke();
                        return;
                    }

                    var targetPosition = default(Vector3);
                    var ourRotation = default(Quaternion);
                    var ourPostion = default(Vector3);
                    lock (_attackCommandExecutor)
                    {
                        targetPosition = _attackCommandExecutor._targetPosition;
                        ourPostion = _attackCommandExecutor._ourPosition;
                        ourRotation = _attackCommandExecutor._ourRotation;
                    }

                    var vector = targetPosition - ourPostion;
                    var distanceToTarget = vector.magnitude;
                    if (distanceToTarget > _attackCommandExecutor._attackDistance)
                    {
                        var finalDistanation = targetPosition - vector.normalized
                            * (_attackCommandExecutor._attackDistance * 0.9f);
                        _attackCommandExecutor._targetPositions.OnNext(finalDistanation);
                        Thread.Sleep(100);
                    }
                    else if (ourRotation != Quaternion.LookRotation(vector))
                    {
                        _attackCommandExecutor._targetRotations.OnNext(Quaternion.LookRotation(vector));
                    }
                    else
                    {
                        _attackCommandExecutor._attackTarget.OnNext(_target);
                        Thread.Sleep(_attackCommandExecutor._attackPeriod);
                    }
                }
            }
            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                return new AttackOperationAwaiter(this);
            }

            #endregion

        }

        #endregion


        #region Fields

        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;

        [Inject] private IHealthHolder _ourHealth;
        [Inject(Id = "AttackDistance")] private float _attackDistance;
        [Inject(Id = "AttackPeriod")] private int _attackPeriod;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IAttackable> _attackTarget = new Subject<IAttackable>();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;

        #endregion


        #region Methods

        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value => new Vector3((float)Math.Round(value.x, 2), (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);

            _attackTarget
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);

            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRotation);
        }

        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;
            _animator.SetTrigger("Walk");
        }

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
            _animator.SetTrigger("Attack");
            target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
        }

        #endregion


        #region ClassLifeCycle

        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            _targetTransform = (command.Target as Component).transform;
            _currentAttackOp = new AttackOperation(this, command.Target);
            Update();
            _stopCommand.CancellationTokenSource = new CancellationTokenSource();

            try
            {
                await _currentAttackOp.WithCancellation(_stopCommand.CancellationTokenSource.Token);
            }
            catch
            {
                _currentAttackOp.Cancel();
            }

            _animator.SetTrigger("Idle");
            _currentAttackOp = null;
            _targetTransform = null;
            _stopCommand.CancellationTokenSource = null;
        }

        private void Update()
        {
            if (_currentAttackOp == null)
            {
                return;
            }

            lock (this)
            {
                _ourPosition = transform.position;
                _ourRotation = transform.rotation;

                if (_targetTransform != null)
                {
                    _targetPosition = _targetTransform.position;
                }
            }
        }

        #endregion

    }
}
