using Domination.Abstractions;
using UnityEngine;
using Zenject;


namespace Domination.Core
{
    public class AttackerParallelnfoUpdater : MonoBehaviour, ITickable
    {

        #region Fields

        [Inject] private IAutomaticAttacker _automatickAttacker;
        [Inject] private ICommandsQueue _queue;

        #endregion


        #region Methods

        public void Tick()
        {
            AutoAttackEvaluator.attackersInfo.AddOrUpdate(
                gameObject,
                new AutoAttackEvaluator.AttackerParallelnfo(_automatickAttacker.VisionRadius, _queue.CurrentCommand),
                (go, value) =>
                {
                    value.visionRadius = _automatickAttacker.VisionRadius;
                    value.currentCommand = _queue.CurrentCommand;
                    return value;
                });
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.attackersInfo.TryRemove(gameObject, out _);
        }

        #endregion

    }
}
