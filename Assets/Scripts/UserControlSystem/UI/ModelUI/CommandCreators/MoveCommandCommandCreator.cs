using Domination.Abstractions;
using Domination.Utils;
using System;
using UnityEngine;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class MoveCommandCommandCreator : CommandCreatorBase<IMoveCommand>
    {

        #region Fields

        [Inject] private AssetsContext _context;

        private Action<IMoveCommand> _creationCallback;

        #endregion


        #region Methods

        [Inject]
        private void Init(Vector3Value groundClicks)
        {
            groundClicks.onNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new MoveCommand(groundClick)));
            _creationCallback = null;
        }
        protected override void ClassSpecificCommandCreator(Action<IMoveCommand> creatonCallback)
        {
            _creationCallback = creatonCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _creationCallback = null;
        }

        #endregion

    }
}