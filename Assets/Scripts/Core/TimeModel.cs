using Domination.Abstractions;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Domination.Core
{
    public class TimeModel : ITimeModel, ITickable
    {

        #region Fields

        private ReactiveProperty<float> _gameTime =
            new ReactiveProperty<float>();

        #endregion


        #region Properties

        public IObservable<int> GameTime => _gameTime.Select(f => (int)f);

        #endregion


        #region Methods

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }

        #endregion

    }
}
