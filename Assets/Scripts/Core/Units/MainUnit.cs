using Domination.Abstractions;
using Domination.Core.CommandExecutor;
using UnityEngine;


namespace Domination.Core
{
    public class MainUnit : MonoBehaviour, ISelecatable, IAttackable, IUnit, IDamageDealer, IAutomaticAttacker
    {

        #region Fields

        [SerializeField] private Animator _animator;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Transform _pivotPoint;
        [SerializeField] private StopCommandExecutor _stopCommand;
        [SerializeField] private int _damage = 25;
        [SerializeField] private float _visionRadius = 8.0f;

        private float _health = 100.0f;

        #endregion


        #region Properties

        public float Health => _health;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;

        public Transform PivotPoint => _pivotPoint;

        public int Damage => _damage;

        public float VisionRadius => _visionRadius;

        #endregion


        #region Methods

        public void RecieveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }

            _health -= amount;
            if (_health <= 0)
            {
                _animator.SetTrigger("Died");
                Invoke(nameof(destroy), 1.0f);
            }
        }

        private async void destroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }

        #endregion

    }
}
