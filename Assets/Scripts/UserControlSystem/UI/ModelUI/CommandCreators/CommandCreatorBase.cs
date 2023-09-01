using Domination.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Domination.UserControlSystem.CommandCreator
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {

        #region Methods

        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;

            if (classSpecificExecutor != null)
            {
                ClassSpecificCommandCreation(callback);
            }

            return commandExecutor;
        }

        protected abstract void ClassSpecificCommandCreation(Action<T> creatonCallback);

        public virtual void ProcessCancel() { }

        #endregion

    }
}
