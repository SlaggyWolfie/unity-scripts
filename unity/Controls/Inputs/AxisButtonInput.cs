using System;
using UnityEngine;

namespace Slaggy.Unity.Controls
{
    [Serializable]
    public class AxisButtonInput : AxisInput, IPressInput
    {
        public AxisButtonInput(string name) : base(name) { }

        public bool Held() => Input.GetButton(name);
        public bool Down() => Input.GetButtonDown(name);
        public bool Up() => Input.GetButtonUp(name);

        public bool IsPositive() => Value() >= 1;
        public bool IsNegative() => Value() <= -1;

        public bool PositiveHeld() => Held() && IsPositive();
        public bool NegativeHeld() => Held() && IsNegative();

        public bool PositiveDown() => Down() && IsPositive();
        public bool NegativeDown() => Down() && IsNegative();

        public bool PositiveUp() => Up() && IsPositive();
        public bool NegativeUp() => Up() && IsNegative();

        private bool Held(out float value, Func<float> input)
        {
            bool held = Held();
            value = held ? input() : 0;
            return held;
        }

        private bool Down(out float value, Func<float> input)
        {
            bool down = Down();
            value = down ? input() : 0;
            return down;
        }

        private bool Up(out float value, Func<float> input)
        {
            bool up = Up();
            value = up ? input() : 0;
            return up;
        }

        public bool Held(out float value) => Held(out value, Value);
        public bool Down(out float value) => Down(out value, Value);
        public bool Up(out float value) => Up(out value, Value);

        public bool HeldRaw(out float value) => Held(out value, RawValue);
        public bool DownRaw(out float value) => Down(out value, RawValue);
        public bool UpRaw(out float value) => Up(out value, RawValue);
    }
}