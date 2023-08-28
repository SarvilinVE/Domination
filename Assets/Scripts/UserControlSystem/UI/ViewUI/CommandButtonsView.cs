using Domination.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Domination.UserControlSystem.UI.View
{
    public class CommandButtonsView : MonoBehaviour
    {

        #region Fields

        public Action<ICommandExecutor> OnClick;

        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _produceButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>()
            {
                {typeof(CommandExecutorBase<IAttackCommand>), _attackButton },
                {typeof(CommandExecutorBase<IMoveCommand>), _moveButton },
                {typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton },
                {typeof(CommandExecutorBase<IStopCommand>), _stopButton },
                {typeof(CommandExecutorBase<IProduceUnitCommand>), _produceButton },
            };
        }

        #endregion


        #region Methods

        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                var buttonGameObject = _buttonsByExecutorType
                    .Where(type => type
                    .Key
                    .IsAssignableFrom(currentExecutor.GetType())
                    )
                    .First()
                    .Value;
                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor));
            }
        }

        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                kvp.Value.SetActive(false);
            }
        }

        #endregion

    }
}
