using Domination.Abstractions;
using Domination.Utils;
using UnityEngine;


namespace Domination.UserControlSystem
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {

        #region Fields

        [InjectAsset("Unit")] private GameObject _unitPrefab;

        #endregion


        #region Properties

        public GameObject UnitPrefab => _unitPrefab;

        #endregion


        #region ClassLifeCycle

        #endregion

    }
}
