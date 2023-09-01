using Domination.Abstractions;
using Domination.Utils;
using System;
using UnityEngine;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {

        #region Fields

        //[Inject] private AssetsContext _context;
        [Inject] private SelectableValue _selectable;

        //private event Action<IPatrolCommand> _creationCallBack;

        #endregion


        #region Methods

        protected override IPatrolCommand CreateCommand(Vector3 argument)
        {
            return new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, argument);
        }

        //[Inject]
        //private void Init(Vector3Value groundClicks)
        //{
        //    groundClicks.OnNewValue += OnNewValue;
        //}

        //private void OnNewValue(Vector3 groundClick)
        //{
        //    _creationCallBack?.Invoke(_context.Inject(new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, groundClick)));
        //    _creationCallBack = null;
        //}

        //protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creatonCallback)
        //{
        //    _creationCallBack = creatonCallback;
        //}

        //public override void ProcessCancel()
        //{
        //    base.ProcessCancel();
        //    _creationCallBack = null;
        //}

        #endregion

    }
}