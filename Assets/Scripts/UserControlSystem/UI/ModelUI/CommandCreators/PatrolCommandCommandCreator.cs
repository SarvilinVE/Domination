using Domination.Abstractions;
using Domination.Utils;
using System;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {

        #region Fields

        [Inject] private AssetsContext _context;

        #endregion


        #region Methods

        protected override void ClassSpecificCommandCreator(Action<IPatrolCommand> creatonCallback)
        {
            creatonCallback?.Invoke(_context.Inject(new PatrolCommand()));
        }

        #endregion

    }
}