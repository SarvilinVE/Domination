using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core.CommandExecutor
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {

        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} attacked {command.Target} with {command.Target.Health} / {command.Target.MaxHealth}");
        }

        #endregion

    }
}
