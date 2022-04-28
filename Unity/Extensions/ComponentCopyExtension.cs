using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

namespace Slaggy.Unity.Extensions
{
    // https://answers.unity.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html
    public static class ComponentCopyExtension
    {
        public static T CopyValuesFrom<T>(this T destinationComponent, T sourceComponent) where T : Component
        {
            Type type = destinationComponent.GetType();

            const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.NonPublic |
                                       BindingFlags.Instance | BindingFlags.Default |
                                       BindingFlags.DeclaredOnly;

            IEnumerable<PropertyInfo> propertyInfos =
                from property in type.GetProperties(FLAGS)
                where property.CustomAttributes.All(attribute => attribute.AttributeType != typeof(ObsoleteAttribute))
                select property;

            foreach (PropertyInfo info in propertyInfos)
            {
                if (!info.CanWrite) continue;

                try
                {
                    info.SetValue(destinationComponent, info.GetValue(sourceComponent, null), null);
                }
                catch (Exception e)
                {
                    // In case of NotImplementedException being thrown.
                    // For some reason specifying that exception didn't seem to catch it,
                    // so I didn't catch anything specific.

                    if (!(e is NotImplementedException))
                        Debug.LogWarning("WARNING: Exception when copying " +
                                         $"properties from ({sourceComponent}) to " +
                                         $"({destinationComponent}). Exception: {e}");
                }
            }

            IEnumerable<FieldInfo> fieldInfos =
                from property in type.GetFields(FLAGS)
                where property.CustomAttributes.All(attribute => attribute.AttributeType != typeof(ObsoleteAttribute))
                select property;


            foreach (FieldInfo info in fieldInfos)
                info.SetValue(destinationComponent, info.GetValue(sourceComponent));

            return destinationComponent;
        }

        public static T CopyAddComponent<T>(this GameObject go, T sourceComponent) where T : Component =>
            go.AddComponent<T>().CopyValuesFrom(sourceComponent);
    }
}