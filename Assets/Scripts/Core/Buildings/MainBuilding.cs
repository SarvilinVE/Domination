using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class MainBuilding : /*CommandExecutorBase<IProduceUnitCommand>*/ MonoBehaviour, ISelecatable, IAttackable
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

        public Vector3 RallyPoint { get; set; }

        #endregion


        #region ClassLifeCycle

        //        public override Task ExecuteSpecificCommand(IProduceUnitCommand command)
        //        {
        //            return Task.CompletedTask;
        ////            Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0,
        ////Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
        //        }

        #endregion


        #region Methods

        public void RecieveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }

            _health -= amount;
            if (_health <= 0 )
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }
}
