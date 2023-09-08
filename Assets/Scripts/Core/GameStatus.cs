using Domination.Abstractions;
using System;
using System.Threading;
using UniRx;
using UnityEngine;


namespace Domination.Core
{
    public class GameStatus : MonoBehaviour, IGameStatus
    {

        #region Fields

        private Subject<int> _status = new Subject<int>();

        #endregion


        #region Properties

        public IObservable<int> Status => _status;

        #endregion


        #region Methods

        private void CheckStatus(object state)
        {
            if (FactionMember.FactionsCount == 0)
            {
                _status.OnNext(0);
            }
            else if (FactionMember.FactionsCount == 1)
            {
                _status.OnNext(FactionMember.GetWinner());
            }
        }

        public void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckStatus);
        }

        #endregion

    }
}
