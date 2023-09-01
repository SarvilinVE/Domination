using Domination.Abstractions;
using UnityEngine;

namespace Domination.UserControlSystem
{
    public class PatrolCommand : IPatrolCommand
    {

        #region Properties

        public Vector3 From { get; }
        public Vector3 To { get; }

        #endregion


        #region Methods

        public PatrolCommand(Vector3 from, Vector3 to)
        {
            From = from; 
            To = to;
        }

        #endregion

    }
}