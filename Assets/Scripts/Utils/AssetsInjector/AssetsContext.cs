using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Domination.Utils
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy Game/" +
nameof(AssetsContext), order = 0)]
    public class AssetsContext : ScriptableObject
    {

        #region Fields

        [SerializeField] private Object[] _objects;

        #endregion


        #region Methods

        public Object GetObjectOfType(Type targetType, string targetName = null)
        {
            
            for (var i = 0; i < _objects.Length; i++)
            {
                var obj = _objects[i];
                if (obj.GetType().IsAssignableFrom(targetType))
                {
                    if (targetName == null || obj.name == targetName)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }

        #endregion

    }
}
