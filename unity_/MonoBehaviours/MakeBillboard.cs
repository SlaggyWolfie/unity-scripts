using UnityEngine;

namespace Slaggy.Unity.UI
{
    public class MakeBillboard : MonoBehaviour
    {
        public new Camera camera = null;

        private Camera GetCamera() => camera ?? Camera.main;

        private void Update()
        {
            transform.forward = (transform.position - GetCamera().transform.position).normalized;

            Vector3 rotationEuler = transform.eulerAngles;
            rotationEuler.y = 0;

            transform.eulerAngles = rotationEuler;
        }
    }
}