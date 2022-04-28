using System;
using UnityEngine;

namespace Slaggy.Unity.Controls
{
    [Serializable]
    public class AxisInput : InputWrapper
    {
        public AxisInput(string name) : base(name) { }

        public float Value() => Input.GetAxis(name);
        public float RawValue() => Input.GetAxisRaw(name);
    }
}