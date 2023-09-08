using Domination.Abstractions;
using Domination.Utils;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


namespace Domination.Core.CommandExecutor
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {

        #region Fields

        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        #endregion


        #region ClassLifeCycle

        public override async Task ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(Animator.StringToHash("Walk"));
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop.WithCancellation
                    (
                    _stopCommandExecutor.CancellationTokenSource.Token
                    );
            }
            catch
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
            }

            _stopCommandExecutor.CancellationTokenSource = null;
            _animator.SetTrigger("Idle");
        }

        #endregion

    }
}