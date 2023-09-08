using Domination.Abstractions;
using UnityEngine;
using Zenject;


namespace Domination.Core
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private AttackerParallelnfoUpdater _attackerParallelnfoUpdater;
        [SerializeField] private FactionMemberParallelInfoUpdater _factionMemberParallelInfoUpdater;

        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().FromComponentInChildren();
            Container.Bind<float>().WithId("AttackDistance").FromInstance(5.0f);
            Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);

            Container.Bind<IAutomaticAttacker>().FromComponentInChildren();
            Container.Bind<ITickable>().FromInstance(_attackerParallelnfoUpdater);
            Container.Bind<ITickable>().FromInstance(_factionMemberParallelInfoUpdater);
            Container.Bind<IFactionMember>().FromComponentInChildren();
            Container.Bind<ICommandsQueue>().FromComponentInChildren();
        }
    }
}
