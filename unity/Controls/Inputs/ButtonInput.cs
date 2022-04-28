using System;
using UnityEngine;

namespace Slaggy.Unity.Controls
{
    [Serializable]
    public class ButtonInput : InputWrapper, IPressInput
    {
        public ButtonInput(string name) : base(name) { }

        public bool Held() => Input.GetButton(name);
        public bool Down() => Input.GetButtonDown(name);
        public bool Up() => Input.GetButtonUp(name);
    }
}