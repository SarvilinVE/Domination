using Domination.Abstractions;
using Domination.Utils;
using System;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {

        #region Fields

        [Inject] private AssetsContext _context;

        #endregion


        #region Methods

        protected override void ClassSpecificCommandCreator(Action<IStopCommand> creatonCallback)
        {
            creatonCallback?.Invoke(_context.Inject(new StopCommand()));
        }

        #endregion

    }
}