using Domination.Abstractions;
using Domination.UserControlSystem.CommandCreator;
using System;
using UnityEngine;
using Zenject;


namespace Domination.UserControlSystem
{
    public class CommandButtonsModel
    {

        #region Fields

        public event Action<ICommandExecutor> onCommandAccepted;
        public event Action onCommandSend;
        public event Action onCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _setRally;

        private bool _commandIsPanding;

        #endregion


        #region Methods

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            if (_commandIsPanding)
            {
                ProcessOnCancel();
            }

            _commandIsPanding = true;
            onCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _setRally.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _mover.ProcessCancel();
            _stopper.ProcessCancel();
            _patroller.ProcessCancel();
            _setRally.ProcessCancel();

            onCommandCancel?.Invoke();
        }

        private void ExecuteCommandWrapper(object command, ICommandsQueue commandsQueue)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                commandsQueue.Cancel();
            }

            commandsQueue.EnqueueCommand(command);
            _commandIsPanding = false;
            onCommandSend?.Invoke();
        }

        public void OnSelectionChange()
        {
            _commandIsPanding = false;
            ProcessOnCancel();
        }

        #endregion

    }
}
