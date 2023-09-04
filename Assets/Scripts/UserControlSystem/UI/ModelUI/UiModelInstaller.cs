using Domination.Abstractions;
using Domination.UserControlSystem;
using Domination.UserControlSystem.CommandCreator;
using Zenject;

public class UiModelInstaller : MonoInstaller
{

    #region Fields

    //[SerializeField] private AssetsContext _legacyContext;
    //[SerializeField] private Vector3Value _groundClicksRMB;

    #endregion


    #region Methods

    public override void InstallBindings()
    {
        //Container.Bind<AssetsContext>().FromInstance(_legacyContext);
        //Container.Bind<Vector3Value>().FromInstance(_groundClicksRMB);

        Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>().AsTransient();
        Container.Bind<float>().WithId("Unit").FromInstance(5.0f);
        Container.Bind<string>().WithId("Unit").FromInstance("Unit");

        Container.Bind<CommandButtonsModel>().AsTransient();
    }

    #endregion

}
