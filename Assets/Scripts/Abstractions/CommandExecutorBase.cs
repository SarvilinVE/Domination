using UnityEngine;


namespace Domination.Abstractions
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor
    {

        #region Methods

        public void ExecuteCommand(object command) => ExecuteSpecificCommand((T)command);

        public abstract void ExecuteSpecificCommand(T command);

        #endregion

    }
}
