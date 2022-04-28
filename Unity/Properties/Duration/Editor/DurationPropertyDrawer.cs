using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Slaggy.Unity.Properties.Editor
{
    [CustomPropertyDrawer(typeof(DurationAttribute))]
    public class DurationPropertyDrawer : PropertyDrawer
    {
        private const int UNITS_LABEL_WIDTH = 100;
        private const DurationUnits DEFAULT_FLEXIBLE_UNIT = DurationUnits.Seconds;

        private static readonly List<DurationUnits> OPTION_UNITS_MODES =
            new List<DurationUnits>
            {
            DurationUnits.Milliseconds,
            DurationUnits.Seconds,
            DurationUnits.Minutes,
            DurationUnits.Hours
            };

        private static readonly string[] DISPLAY_OPTIONS = OPTION_UNITS_MODES.Select(u => u.ToString()).ToArray();

        private DurationUnits _units = DurationUnits.Flexible;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Float)
            {
                EditorGUI.LabelField(position, label.text, "Use the Duration Attribute with Floats only.");
                return;
            }

            DurationUnits units = ((DurationAttribute) attribute).Units;

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;

            if (units != DurationUnits.Flexible)
            {
                float displayValue = ConvertToUnits(property.floatValue, units);
                int width = UnitsTextWidth(units);

                Rect sliderPosition = new Rect(
                    position.min.x,
                    position.min.y,
                    position.width - width,
                    position.height);

                displayValue = EditorGUI.Slider(sliderPosition, displayValue, 0f, UnitsMax(units));

                Rect labelPosition = new Rect(
                    position.max.x - width,
                    position.y,
                    width,
                    position.height);

                EditorGUI.LabelField(labelPosition, $" {units}");
                property.floatValue = ConvertFromUnits(displayValue, units);
            }
            else
            {
                if (_units == DurationUnits.Flexible) _units = DEFAULT_FLEXIBLE_UNIT;

                // Slider & Number
                {
                    float displayValue = ConvertToUnits(property.floatValue, _units);

                    Rect sliderPosition = new Rect(
                        position.min.x,
                        position.min.y,
                        position.width - UNITS_LABEL_WIDTH,
                        position.height);

                    displayValue = EditorGUI.Slider(sliderPosition, displayValue, 0f, UnitsMax(_units));

                    Rect labelPosition = new Rect(
                        position.max.x - UNITS_LABEL_WIDTH,
                        position.y,
                        UNITS_LABEL_WIDTH,
                        position.height);

                    EditorGUI.LabelField(labelPosition, $" {_units}");
                    property.floatValue = ConvertFromUnits(displayValue, _units);
                }

                // Dropdown & Units
                {
                    int selectedIndex = OPTION_UNITS_MODES.IndexOf(_units);

                    Rect dropdownPosition = new Rect(
                        position.min.x + position.width - UNITS_LABEL_WIDTH,
                        position.y,
                        UNITS_LABEL_WIDTH,
                        position.height);

                    selectedIndex = EditorGUI.Popup(dropdownPosition, selectedIndex, DISPLAY_OPTIONS);
                    _units = OPTION_UNITS_MODES[selectedIndex];
                }
            }

            EditorGUI.EndProperty();
        }

        private static float ConvertToUnits(float f, DurationUnits type)
        {
            switch (type)
            {
                case DurationUnits.Flexible:
                    Debug.LogError("ConvertToUnits cannot be used with FloatDurationUnitsMode.Flexible.");
                    return f;

                case DurationUnits.Minutes: return f / 60;
                case DurationUnits.Hours: return f / 3600;
                case DurationUnits.Milliseconds: return f * 1000;
                case DurationUnits.Seconds:
                default: return f;
            }
        }

        private static float ConvertFromUnits(float f, DurationUnits type)
        {
            switch (type)
            {
                case DurationUnits.Flexible:
                    Debug.LogError("ConvertToUnits cannot be used with FloatDurationUnitsMode.Flexible.");
                    return f;

                case DurationUnits.Minutes: return f * 60;
                case DurationUnits.Hours: return f * 3600;
                case DurationUnits.Milliseconds: return f / 1000;
                case DurationUnits.Seconds:
                default: return f;
            }
        }

        private static float UnitsMax(DurationUnits type)
        {
            switch (type)
            {
                case DurationUnits.Seconds: return 10;
                case DurationUnits.Milliseconds: return 1000;
                case DurationUnits.Minutes: return 5;
                case DurationUnits.Hours: return 4;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static int UnitsTextWidth(DurationUnits type)
        {
            switch (type)
            {
                case DurationUnits.Seconds: return 55;
                case DurationUnits.Milliseconds: return 75;
                case DurationUnits.Minutes: return 50;
                case DurationUnits.Hours: return 38;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}