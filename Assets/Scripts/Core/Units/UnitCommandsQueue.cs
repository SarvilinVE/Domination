using Domination.Abstractions;
using Domination.Utils;
using UniRx;
using UnityEngine;
using Zenject;


namespace Domination.Core
{
    public class UnitCommandsQueue : MonoBehaviour, ICommandsQueue
    {

        #region Fields

        [Inject] CommandExecutorBase<IMoveCommand> _moveCommandExecutor;
        [Inject] CommandExecutorBase<IAttackCommand> _attackCommandExecutor;
        [Inject] CommandExecutorBase<IStopCommand> _stopCommandExecutor;
        [Inject] CommandExecutorBase<IPatrolCommand> _patrolCommandExecutor;

        private ReactiveCollection<ICommand> _innerCollection = new ReactiveCollection<ICommand>();

        #endregion


        #region Properties

        public ICommand CurrentCommand => _innerCollection.Count > 0 ? _innerCollection[0] : default;

        #endregion


        #region Methods

        [Inject]
        private void Init()
        {
            _innerCollection
                .ObserveAdd().Subscribe(OnNewCommand).AddTo(this);
        }

        private void OnNewCommand(ICommand command, int index)
        {
            if (index == 0)
            {
                ExecuteCommand(command);
            }
        }

        private async void ExecuteCommand(ICommand command)
        {
            await _moveCommandExecutor.TryExecuteCommand(command);
            await _attackCommandExecutor.TryExecuteCommand(command);
            await _stopCommandExecutor.TryExecuteCommand(command);
            await _patrolCommandExecutor.TryExecuteCommand(command);

            if (_innerCollection.Count> 0)
            {
                _innerCollection.RemoveAt(0);
            }

            CheckTheQueue();
        }

        private void CheckTheQueue()
        {
            if (_innerCollection.Count > 0)
            {
                ExecuteCommand(_innerCollection[0]);
            }
        }

        public void EnqueueCommand(object wrappedCommand)
        {
            var command = wrappedCommand as ICommand;
            _innerCollection.Add(command);
        }

        public void Cancel()
        {
            _innerCollection.Clear();
            _stopCommandExecutor.ExecuteSpecificCommand(new StopCommand());
        }

        #endregion

    }
}
