using System;

namespace Slaggy.Unity.Controls
{
    [Serializable]
    public class InputWrapper
    {
        public readonly string name;
        public InputWrapper(string name) => this.name = name;

        public static implicit operator string(InputWrapper iw) => iw.name;
        //public static implicit operator InputWrapper(string text) => new InputWrapper(text);
    }

    public interface IPressInput { bool Held(); bool Down(); bool Up(); }
}