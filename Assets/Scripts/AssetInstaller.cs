using Domination.Abstractions;
using Domination.UserControlSystem;
using Domination.Utils;
using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "AssetInstaller", menuName = "Installers/AssetInstaller")]
public class AssetInstaller : ScriptableObjectInstaller<AssetInstaller>
{

    #region Fields

    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private SelectableValue _selectables;
    [SerializeField] private AttackableValue _attackableClickRMB;

    #endregion


    #region Methods

    public override void InstallBindings()
    {
        Container.BindInstances(_legacyContext, _groundClicksRMB, _selectables, _attackableClickRMB);
        Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableClickRMB);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClicksRMB);
    }

    #endregion

}