using Domination.Abstractions;
using Domination.Utils;
using System;
using UnityEngine;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {

        #region Fields

        //[Inject] private AssetsContext _context;

        //private Action<IMoveCommand> _creationCallback;

        #endregion


        #region Methods

        protected override IMoveCommand CreateCommand(Vector3 argument)
        {
            return new MoveCommand(argument);
        }

        //[Inject]
        //private void Init(Vector3Value groundClicks)
        //{
        //    groundClicks.OnNewValue += OnNewValue;
        //}

        //private void OnNewValue(Vector3 groundClick)
        //{
        //    _creationCallback?.Invoke(_context.Inject(new MoveCommand(groundClick)));
        //    _creationCallback = null;
        //}
        //protected override void ClassSpecificCommandCreation(Action<IMoveCommand> creatonCallback)
        //{
        //    _creationCallback = creatonCallback;
        //}

        //public override void ProcessCancel()
        //{
        //    base.ProcessCancel();

        //    _creationCallback = null;
        //}

        #endregion

    }
}