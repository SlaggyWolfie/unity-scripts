using System;
using UnityEngine;

namespace Slaggy.Unity.Controls
{
    [Serializable]
    public class KeyCodeInput : IPressInput
    {
        public readonly KeyCode key;
        public KeyCodeInput(KeyCode key) => this.key = key;

        public bool Held() => Input.GetKey(key);
        public bool Down() => Input.GetKeyDown(key);
        public bool Up() => Input.GetKeyUp(key);
    }
}