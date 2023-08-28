using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core.CommandExecutor
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {

        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"Attack command");
        }

        #endregion

    }
}
