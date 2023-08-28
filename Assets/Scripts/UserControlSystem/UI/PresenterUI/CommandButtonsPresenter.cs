using Domination.Abstractions;
using Domination.UserControlSystem.UI.View;
using Domination.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Domination.UserControlSystem.UI.Presenter
{
    public class CommandButtonsPresenter : MonoBehaviour
    {

        #region Fields

        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelecatable _currentSelectable;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);
            _view.OnClick += OnButtonClick;
        }

        #endregion


        #region Methods

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;

            if (unitProducer != null)
            {
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommand()));
                return;
            }

            var attacker = commandExecutor as CommandExecutorBase<IAttackCommand>;

            if (attacker != null)
            {
                attacker.ExecuteSpecificCommand(_context.Inject(new AttackCommand()));
                return;
            }

            var stopper = commandExecutor as CommandExecutorBase<IStopCommand>;

            if (stopper != null)
            {
                stopper.ExecuteSpecificCommand(_context.Inject(new StopCommand()));
                return;
            }

            var patroller = commandExecutor as CommandExecutorBase<IPatrolCommand>;

            if (patroller != null)
            {
                patroller.ExecuteSpecificCommand(_context.Inject(new PatrolCommand()));
                return;
            }

            var mover = commandExecutor as CommandExecutorBase<IMoveCommand>;

            if (mover != null)
            {
                mover.ExecuteSpecificCommand(_context.Inject(new MoveCommand()));
                return;
            }

            throw new
                ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)} Unknown type of commands executor: {commandExecutor.GetType().FullName}");
        }

        private void OnSelected(ISelecatable selecatable)
        {
            if (_currentSelectable == selecatable)
            {
                return;
            }

            _currentSelectable = selecatable;
            _view.Clear();

            if (selecatable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selecatable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        #endregion

    }
}
