using Domination.Abstractions;
using UnityEngine;
using Zenject;


namespace Domination.Core
{
    public class FactionMemberParallelInfoUpdater : MonoBehaviour, ITickable
    {

        #region Fields

        [Inject] private IFactionMember _factionMember;

        #endregion


        #region Methods

        public void Tick()
        {
            AutoAttackEvaluator.factionMambersInfo.AddOrUpdate(
                gameObject,
                new AutoAttackEvaluator.FactionMemberParallelInfo(transform.position, _factionMember.FactionId),
                (go, value) =>
                {
                    value.position = transform.position;
                    value.faction = _factionMember.FactionId;
                    return value;
                });
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.factionMambersInfo.TryRemove(gameObject, out _);
        }

        #endregion

    }
}
