using UnityEngine;

public class EditorUtils
{
    public static Vector2 CalculateSizeAsLabel(string text)
    {
        GUIStyle style = GUI.skin.label;
        GUIContent content = new GUIContent(text);

        return style.CalcSize(content);
    }
}
