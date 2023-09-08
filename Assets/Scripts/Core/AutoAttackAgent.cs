using Domination.Abstractions;
using UniRx;
using UnityEngine;


namespace Domination.Core
{
    public class AutoAttackAgent : MonoBehaviour
    {

        #region Fields

        [SerializeField] private UnitCommandsQueue _queue;

        #endregion


        #region UnityMethods

        private void Start()
        {
            AutoAttackEvaluator.autoAttackCommands
                .ObserveOnMainThread()
                .Where(command => command.attacker == gameObject)
                .Where(command => command.attacker != null && command.target != null)
                .Subscribe(command => AutoAttack(command.target))
                .AddTo(this);
        }

        #endregion


        #region MEthods

        private void AutoAttack(GameObject target)
        {
            _queue.Cancel();
            _queue.EnqueueCommand(new AutoAttackCommand(target.GetComponent<IAttackable>()));
        }

        #endregion

    }
}
