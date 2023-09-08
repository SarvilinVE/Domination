using Domination.Abstractions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Domination.Core
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {

        #region Fields

        [SerializeField] private int _factionId;

        private static Dictionary<int, List<int>> _membersCount = new Dictionary<int, List<int>>();

        #endregion


        #region Properties

        public int FactionId => _factionId;
        public static int FactionsCount
        {
            get
            {
                lock(_membersCount )
                {
                    return _membersCount.Count;
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (_factionId != 0)
            {
                Register();
            }
        }

        private void OnDestroy()
        {
            Unregister();
        }

        #endregion


        #region Methods

        public static int GetWinner()
        {
            lock (_membersCount)
            {
                return _membersCount.Keys.First();
            }
        }

        public void SetFaction(int factionId)
        {
            _factionId = factionId;
        }

        private void Register()
        {
            lock(_membersCount )
            {
                if (!_membersCount.ContainsKey(_factionId))
                {
                    _membersCount.Add(_factionId, new List<int>());
                }
                if (!_membersCount[_factionId].Contains(GetInstanceID()))
                {
                    _membersCount[_factionId].Add(GetInstanceID());
                }
            }
        }

        private void Unregister()
        {
            lock (_membersCount )
            {
                if (_membersCount[_factionId].Contains(GetInstanceID()))
                {
                    _membersCount[_factionId].Remove(GetInstanceID());
                }
                if (_membersCount[_factionId].Count == 0)
                {
                    _membersCount.Remove(_factionId);
                }
            }
        }

        #endregion

    }
}
