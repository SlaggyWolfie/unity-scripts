using UnityEditor;
using UnityEngine;

namespace Slaggy.Unity.Fields.Editor
{
    [CustomPropertyDrawer(typeof(Layer))]
    public class LayerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            SerializedProperty layerIndex = property.FindPropertyRelative("_index");
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            layerIndex.intValue = EditorGUI.LayerField(position, layerIndex.intValue);

            EditorGUI.EndProperty();
        }
    }
}