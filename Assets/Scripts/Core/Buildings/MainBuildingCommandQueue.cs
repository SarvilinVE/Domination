using Domination.Abstractions;
using UnityEngine;
using Zenject;


namespace Domination.Core
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {

        #region Fields

        [Inject] private CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
        [Inject] private CommandExecutorBase<ISetRallyPointCommand> _setRallyCommandExecutor;

        #endregion


        #region Properties

        public ICommand CurrentCommand => default;

        #endregion


        #region Methods

        public void Cancel() { }

        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
            await _setRallyCommandExecutor.TryExecuteCommand(command);
        }

        #endregion

    }
}
