using UnityEngine;

namespace Slaggy.Utility
{
    public class DisplayFPS : MonoBehaviour
    {
        private float deltaTime = 0.0f;

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            int width = Screen.width, height = Screen.height;

            GUIStyle style = new GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = height * 2 / 100,
                normal =
            {
                textColor = new Color(255, 181, 47, 1.0f)
            }
            };

            float milliseconds = deltaTime * 1000.0f;
            float framesPerSecond = 1.0f / deltaTime;
            string text = $"{milliseconds:0.0} ms \n({framesPerSecond:0.} FPS)";

            Rect rect = new Rect(0, 0, width, height * 4 / 100.0f);
            GUI.Label(rect, text, style);
        }
    }
}