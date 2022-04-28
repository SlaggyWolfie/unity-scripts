using System;
using UnityEngine;

namespace Slaggy.Unity.Controls
{
    [Serializable]
    public class KeyInput : InputWrapper, IPressInput
    {
        public KeyInput(string name) : base(name) { }

        public bool Held() => Input.GetKey(name);
        public bool Down() => Input.GetKeyDown(name);
        public bool Up() => Input.GetKeyUp(name);
    }
}