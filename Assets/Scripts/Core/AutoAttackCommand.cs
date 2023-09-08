using Domination.Abstractions;


namespace Domination.Core
{
    public class AutoAttackCommand : IAttackCommand
    {

        #region Properties

        public IAttackable Target { get; }

        #endregion


        #region ClassLifeCycle

        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }

        #endregion

    }
}
