using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class MainUnit : MonoBehaviour, ISelecatable, IAttackable
    {

        #region Fields

        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Transform _pivotPoint;

        private float _health = 100.0f;

        #endregion


        #region Properties

        public float Health => _health;

        public float MaxHealth => _maxHealth;

        public Sprite Icon => _icon;

        public Transform PivotPoint => _pivotPoint;

        #endregion

    }
}
