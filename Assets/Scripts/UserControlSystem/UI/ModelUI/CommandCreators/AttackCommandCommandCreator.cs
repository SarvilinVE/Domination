using Domination.Abstractions;
using Domination.Utils;
using System;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {

        #region Fields

        [Inject] private AssetsContext _context;

        #endregion


        #region Methods

        protected override void ClassSpecificCommandCreator(Action<IAttackCommand> creatonCallback)
        {
            creatonCallback?.Invoke(_context.Inject(new AttackCommand()));
        }

        #endregion

    }
}