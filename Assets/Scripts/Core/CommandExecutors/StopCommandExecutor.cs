using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core.CommandExecutor
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {

        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"Stop command");
        }

        #endregion

    }
}