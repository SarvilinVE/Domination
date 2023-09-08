using Domination.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;


namespace Domination.Core
{
    public class AutoAttackEvaluator : MonoBehaviour
    {

        #region FactionMemberParallelInfo

        public class FactionMemberParallelInfo
        {

            #region Fields

            public Vector3 position;
            public int faction;

            #endregion


            #region Methods

            public FactionMemberParallelInfo(Vector3 position, int faction)
            {
                this.position = position;
                this.faction = faction;
            }

            #endregion

        }

        #endregion


        #region AttackerParallelnfo

        public class AttackerParallelnfo
        {

            #region Fields

            public float visionRadius;
            public ICommand currentCommand;

            #endregion


            #region Methods

            public AttackerParallelnfo(float visionRadius, ICommand currentCommand)
            {
                this.visionRadius = visionRadius;
                this.currentCommand = currentCommand;
            }

            #endregion

        }

        #endregion


        #region Command

        public class Command
        {

            #region Fields

            public GameObject attacker;
            public GameObject target;

            #endregion


            #region ClassLifeCycle

            public Command(GameObject attacker, GameObject target)
            {
                this.attacker = attacker;
                this.target = target;
            }

            #endregion

        }

        #endregion


        #region Fields

        public static ConcurrentDictionary<GameObject, AttackerParallelnfo> attackersInfo
            = new ConcurrentDictionary<GameObject, AttackerParallelnfo>();

        public static ConcurrentDictionary<GameObject, FactionMemberParallelInfo> factionMambersInfo
            = new ConcurrentDictionary<GameObject, FactionMemberParallelInfo>();

        public static Subject<Command> autoAttackCommands = new Subject<Command>();

        #endregion


        #region UnityMethods

        private void Update()
        {
            Parallel.ForEach(attackersInfo, kvp => Evaluate(kvp.Key, kvp.Value));
        }

        #endregion


        #region Methods

        private void Evaluate(GameObject go, AttackerParallelnfo info)
        {
            if (info.currentCommand is IMoveCommand)
            {
                return;
            }

            if (info.currentCommand is IAttackCommand && !(info.currentCommand is Command))
            {
                return;
            }

            var factionInfo = default(FactionMemberParallelInfo);
            if (!factionMambersInfo.TryGetValue(go, out factionInfo))
            {
                return;
            }

            foreach (var (otherGo,otherFactionInfo) in factionMambersInfo)
            {
                if (factionInfo.faction == otherFactionInfo.faction)
                {
                    continue;
                }

                var distance = Vector3.Distance(factionInfo.position, otherFactionInfo.position);
                if (distance > info.visionRadius)
                {
                    continue;
                }

                autoAttackCommands.OnNext(new Command(go, otherGo));
                break;
            }
        }

        #endregion

    }
}
