using UnityEditor;

using UnityEngine;

namespace Slaggy.Unity.Properties.Editor
{
    [CustomPropertyDrawer(typeof(SuffixAttribute))]
    public class SuffixPropertyDrawer : PropertyDrawer
    {
        private string _suffix = string.Empty;
        private float _width = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // init
            if (_width < 0)
            {
                string suffixText = ((SuffixAttribute) attribute).Suffix;
                _suffix = ' ' + suffixText;

                _width = string.IsNullOrEmpty(suffixText) ? 0 : EditorUtils.CalculateSizeAsLabel(_suffix).x;
            }

            if (_width <= 0) return;

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.indentLevel = 0;

            // draw
            Rect normalPosition = new Rect(position.x, position.y, position.width - _width, position.height);
            // EditorGUI.PropertyField(normalPosition, property);
            EditorGUI.PropertyField(normalPosition, property, label);
                    
            // Suffix Spacing
            Rect suffixPosition = new Rect(
                normalPosition.max.x,
                normalPosition.y,
                _width,
                normalPosition.height);

            EditorGUI.LabelField(suffixPosition, _suffix);

            EditorGUI.EndProperty();
        }
    }
}