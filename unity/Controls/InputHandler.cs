using UnityEngine;

namespace Slaggy.Unity.Controls
{
    public abstract class InputHandler : MonoBehaviour
    {
        public bool allowInput = true;

        protected void Update() { if (allowInput) OnUpdate(); }
        protected abstract void OnUpdate();
    }
}