using Domination.Abstractions;
using Domination.Utils;
using UnityEngine;
using Zenject;

namespace Domination.UserControlSystem
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {

        #region Fields

        [Inject (Id = "Unit")] public string UnitName { get; }
        [Inject (Id = "Unit")] public Sprite Icon { get; }
        [Inject (Id = "Unit")] public float ProductionTime { get; }

        [InjectAsset("Unit")] private GameObject _unitPrefab;

        #endregion


        #region Properties

        public GameObject UnitPrefab => _unitPrefab;

        #endregion


        #region ClassLifeCycle

        #endregion

    }
}
