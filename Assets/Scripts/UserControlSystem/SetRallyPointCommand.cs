using Domination.Abstractions;
using UnityEngine;


namespace Domination.UserControlSystem
{
    public class SetRallyPointCommand : ISetRallyPointCommand
    {

        #region Properties

        public Vector3 RallyPoint { get; }

        #endregion


        #region ClassLifeCycle

        public SetRallyPointCommand(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }

        #endregion

    }
}
