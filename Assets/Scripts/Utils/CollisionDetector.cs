using System;
using UniRx;
using UnityEngine;


namespace Domination.Utils
{
    public class CollisionDetector : MonoBehaviour
    {

        #region Fields

        private Subject<Collision> _collisions = new Subject<Collision>();

        #endregion


        #region Proeprties

        public IObservable<Collision> Collisions => _collisions;

        #endregion


        #region UnityMethods

        private void OnCollisionStay(Collision collision)
        {
            _collisions.OnNext(collision);
        }

        #endregion
    }
}
