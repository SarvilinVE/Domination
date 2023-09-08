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

        public Action<ICommandExecutor, ICommandsQueue> OnClick;

        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _produceButton;
        [SerializeField] private GameObject _setRallyButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>()
            {
                {typeof(ICommandExecutor<IAttackCommand>), _attackButton },
                {typeof(ICommandExecutor<IMoveCommand>), _moveButton },
                {typeof(ICommandExecutor<IPatrolCommand>), _patrolButton },
                {typeof(ICommandExecutor<IStopCommand>), _stopButton },
                {typeof(ICommandExecutor<IProduceUnitCommand>), _produceButton },
                {typeof(ICommandExecutor<ISetRallyPointCommand>), _setRallyButton },
            };
        }

        #endregion


        #region Methods

        public void BlockInteractions(ICommandExecutor commandExecutor)
        {
            UnblockAllInteractions();
            GetButtonGameObjectByType(commandExecutor.GetType()).GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllInteractions() => SetInteractable(true);

        public void SetInteractable(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _produceButton.GetComponent<Selectable>().interactable = value;
            _setRallyButton.GetComponent<Selectable>().interactable = value;
        }

        public GameObject GetButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsByExecutorType
                .Where(type =>
                type.Key.IsAssignableFrom(executorInstanceType))
                .First()
                .Value;
        }

        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors, ICommandsQueue queue)
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
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, queue));
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
