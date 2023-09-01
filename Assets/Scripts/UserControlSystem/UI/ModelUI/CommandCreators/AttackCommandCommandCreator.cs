using Domination.Abstractions;
using Domination.Utils;
using System;
using System.Threading;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {

        #region Fields

        //[Inject] private AssetsContext _context;
        //[Inject] private AttackableValue _groundClicks;

        ////private event Action<IAttackCommand> _creationCallback;
        //private CancellationTokenSource _ctSource;

        #endregion


        #region Methods

        protected override IAttackCommand CreateCommand(IAttackable argument)
        {
            return new AttackCommand(argument);
        }

        //[Inject]
        //private void Init(AttackableValue groundClicks)
        //{
        //    groundClicks.OnNewValue += OnNewValue;
        //}

        //private void OnNewValue(IAttackable attackable)
        //{
        //    _creationCallback?.Invoke(_context.Inject(new AttackCommand(attackable)));
        //    _creationCallback = null;
        //}

        //protected override async void ClassSpecificCommandCreation(Action<IAttackCommand> creatonCallback)
        //{
        //    //_creationCallback = creatonCallback;
        //    //creatonCallback?.Invoke(_context.Inject(new AttackCommand(await _groundClicks)));

        //    _ctSource = new CancellationTokenSource();
        //    try
        //    {
        //        var attackable = await _groundClicks.WithCancellation(_ctSource.Token);
        //        creatonCallback?.Invoke(_context.Inject(new AttackCommand(attackable)));
        //    }
        //    catch { }
        //}

        //public override void ProcessCancel()
        //{
        //    base.ProcessCancel();
        //    //_creationCallback = null;

        //    if (_ctSource != null )
        //    {
        //        _ctSource.Cancel();
        //        _ctSource.Dispose();
        //        _ctSource = null;
        //    }
        //}

        #endregion

    }
}