using Domination.Abstractions;
using System.Threading.Tasks;
using UnityEngine;


namespace Domination.Core
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<T> where T : class,ICommand
    {

        #region Methods

        //public void ExecuteCommand(object command) => ExecuteSpecificCommand((T)command);

        public abstract Task ExecuteSpecificCommand(T command);

        public async Task TryExecuteCommand(object command)
        {
            var specificCommand = command as T;
            if (specificCommand != null)
            {
                await ExecuteSpecificCommand(specificCommand);
            }
        }

        #endregion

    }
}
