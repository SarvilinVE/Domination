using Domination.Abstractions;
using Domination.UserControlSystem.CommandCreator;
using System;
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

        private bool _commandIsPanding;

        #endregion


        #region Methods

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
        {
            if (_commandIsPanding)
            {
                ProcessOnCancel();
            }

            _commandIsPanding = true;
            onCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
            _attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
            _mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
            _stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
            _patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _mover.ProcessCancel();
            _stopper.ProcessCancel();
            _patroller.ProcessCancel();

            onCommandCancel?.Invoke();
        }

        private void ExecuteCommandWrapper(ICommandExecutor commandExecutor, object command)
        {
            commandExecutor.ExecuteCommand(command);
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
