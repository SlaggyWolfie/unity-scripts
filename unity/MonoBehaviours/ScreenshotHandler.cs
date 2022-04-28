using Slaggy.Unity.Singletons;

using UnityEngine;

namespace Slaggy.Unity.Utility
{
    public class ScreenshotHandler : SingletonMonoBehaviour<ScreenshotHandler>
    {
        private Camera _camera = null;
        private bool _takeOnNextFrame = false;

        protected override void Awake()
        {
            base.Awake();
            _camera = gameObject.GetComponent<Camera>();
        }

        private void OnPostRender()
        {
            if (!_takeOnNextFrame) return;

            _takeOnNextFrame = false;
            RenderTexture renderTexture = _camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved 'CameraScreenshot.png'");

            RenderTexture.ReleaseTemporary(renderTexture);
            _camera.targetTexture = null;
        }

        public void TakeScreenshot(int width, int height)
        {
            _camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
            _takeOnNextFrame = true;
        }

        // public static void TakeScreenshot_Static(int width, int height) { instance.TakeScreenshot(width, height); }
    }
}