using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelecatable
    {

        #region Fields

        [SerializeField] private Transform _unitsParent;

        [SerializeField] private float _maxHealth;
        [SerializeField] Sprite _icon;

        private float _health = 1000.0f;

        #endregion


        #region Properties

        public float Health => _health;

        public float MaxHeath => _maxHealth;

        public Sprite Icon => _icon;

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
