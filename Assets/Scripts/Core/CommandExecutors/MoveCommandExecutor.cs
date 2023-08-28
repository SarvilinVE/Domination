using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core.CommandExecutor
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {

        #region ClassLifeCycle

        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"Move command");
        }

        #endregion

    }
}