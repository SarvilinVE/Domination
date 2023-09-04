using Domination.Abstractions;
using UnityEngine;


namespace Domination.Core
{
    public class UnitProductionTask : IUnitProductionTask
    {

        #region Properties

        public string UnitName { get; }

        public float TimeLeft { get; set; }

        public float ProductionTime { get; }

        public Sprite Icon { get; }

        public GameObject UnitPrefab { get; }

        #endregion


        #region ClassLifeCycle

        public UnitProductionTask(float time, string name, GameObject prefab, Sprite icon)
        {
            UnitName = name;
            TimeLeft = time;
            ProductionTime = time;
            Icon = icon;
            UnitPrefab = prefab;
        }

        #endregion

    }
}
