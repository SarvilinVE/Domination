using Domination.Abstractions;
using Domination.UserControlSystem.CommandCreator;
using UnityEngine;


namespace Domination.UserControlSystem
{
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {

        #region Fields

        protected override ISetRallyPointCommand CreateCommand(Vector3 argument)
        {
            return new SetRallyPointCommand(argument);
        }

        #endregion

    }
}
