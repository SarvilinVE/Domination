using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core.CommandExecutor
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {

        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"Patrol command");
        }

        #endregion

    }
}