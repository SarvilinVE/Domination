using Domination.Abstractions;
using System;
using UniRx;
using UnityEngine;
using Zenject;


namespace Domination.UserControlSystem
{
    public class BottomCenterModel 
    {

        #region Properties

        public IObservable<IUnitProducer> UnitProducer { get; private set; }

        #endregion


        #region Methods

        [Inject]
        public void Init(IObservable<ISelecatable> currentSelected)
        {
            UnitProducer = currentSelected
                .Select(selectable => selectable as Component)
                .Select(component =>
                component?.GetComponent<IUnitProducer>());
        }

        #endregion

    }
}
