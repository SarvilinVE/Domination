using Domination.Abstractions;
using Domination.Utils;
using System;
using Zenject;

namespace Domination.UserControlSystem.CommandCreator
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {

        #region Fields

        [Inject] private AssetsContext _context;

        #endregion


        #region Methods

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creatonCallback)
        {
            creatonCallback?.Invoke(_context.Inject(new ProduceUnitCommand()));
        }

        #endregion

    }
}
