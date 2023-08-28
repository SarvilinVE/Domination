using System;
using UnityEngine;
using System.Reflection;

namespace Domination.Utils
{
    public static class AssetsInjector
    {

        #region Fields

        public static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

        #endregion


        #region Methods

        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();
            var allFields = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            for (int i = 0; i < allFields.Length; i++)
            {
                var fieldInfo = allFields[i];
                var injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                if (injectAssetAttribute == null)
                {
                    continue;
                }
                var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                fieldInfo.SetValue(target, objectToInject);
            }

            return target;
        }

        #endregion

    }
}
