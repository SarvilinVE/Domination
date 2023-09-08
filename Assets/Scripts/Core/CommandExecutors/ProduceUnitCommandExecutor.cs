using Domination.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;


namespace Domination.Core
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {

        #region Fields

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        private ReactiveCollection<IUnitProductionTask> _queue = 
            new ReactiveCollection<IUnitProductionTask>();

        [Inject] private DiContainer _diContainer;

        #endregion


        #region Properties

        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (_queue.Count == 0) return;

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeLeft -= Time.deltaTime;

            if (innerTask.TimeLeft <= 0)
            {
                RemoveTaskAtIndex(0);
                var instance = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, 
                    new Vector3(Random.Range(-10, 10), 0.0f, Random.Range(-10, 10)), 
                    Quaternion.identity, _unitsParent);

                var queue = instance.GetComponent<ICommandsQueue>();
                var mainBuilding = GetComponent<MainBuilding>();
                queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));

                var factionMember = instance.GetComponent<FactionMember>();
                factionMember.SetFaction(GetComponent<FactionMember>().FactionId);
            }
        }

        #endregion


        #region Methods

        public void Cancel(int index) => RemoveTaskAtIndex(index);

        private void RemoveTaskAtIndex(int index)
        {
            for (var i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }

            _queue.RemoveAt(_queue.Count - 1);
        }

        public override Task ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if (_queue.Count >= _maximumUnitsInQueue) return Task.CompletedTask;
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.UnitName, command.UnitPrefab, command.Icon));
            return Task.CompletedTask;
        }

        #endregion

    }
}
