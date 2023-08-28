using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class MainUnit : MonoBehaviour, ISelecatable
    {

        #region Fields

        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;

        private float _health = 100.0f;

        #endregion


        #region Properties

        public float Health => _health;

        public float MaxHeath => _maxHealth;

        public Sprite Icon => _icon;

        #endregion

    }
}
