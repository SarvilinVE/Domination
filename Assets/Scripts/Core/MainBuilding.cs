using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelecatable, IAttackable
    {

        #region Fields

        [SerializeField] private Transform _unitsParent;

        [SerializeField] private float _maxHealth;
        [SerializeField] Sprite _icon;
        [SerializeField] private Transform _pivotPoint;

        private float _health = 1000.0f;

        #endregion


        #region Properties

        public float Health => _health;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;

        public Transform PivotPoint => _pivotPoint;

        #endregion


        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0,
Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
        }

        #endregion

    }
}
