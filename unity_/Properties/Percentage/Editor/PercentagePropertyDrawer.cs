using UnityEditor;

using UnityEngine;

namespace Slaggy.Unity.Properties.Editor
{
    [CustomPropertyDrawer(typeof(PercentageAttribute))]
    public class PercentagePropertyDrawer : PropertyDrawer
    {
        private const string SYMBOL = " %";
        // private const float MARGIN = 5;
        private float _symbolWidth = -1;

        private float SymbolWidth
        {
            get
            {
                if (_symbolWidth > 0) return _symbolWidth;

                _symbolWidth = EditorUtils.CalculateSizeAsLabel(SYMBOL).x;
                return _symbolWidth;
            }
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Float)
            {
                EditorGUI.LabelField(position, label.text, "Use the Percentage Attribute with Floats only.");
                return;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;

            // Slider + Number
            // convert input
            float displayValue = property.floatValue * 100;

            // draw
            Rect sliderPosition = new Rect(position.min.x, position.min.y, position.width - SymbolWidth, position.height);
            displayValue = EditorGUI.Slider(sliderPosition, displayValue, 0, 100);

            // convert output (funky rule here)
            if (displayValue < 1) property.floatValue = displayValue;
            else property.floatValue = displayValue / 100;

            // Percentage Symbol
            Rect symbolPosition = new Rect(
                sliderPosition.max.x,
                sliderPosition.y,
                SymbolWidth,
                sliderPosition.height);

            EditorGUI.LabelField(symbolPosition, "%");

            EditorGUI.EndProperty();
        }
    }
}