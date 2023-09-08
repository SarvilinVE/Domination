using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class MoveCommand : IMoveCommand
    {

        #region Properties

        public Vector3 Target { get; }

        #endregion


        #region ClassLifeCycle

        public MoveCommand(Vector3 target)
        {
            Target = target;
        }

        #endregion

    }
}