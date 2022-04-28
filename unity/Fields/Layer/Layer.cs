using System;
using UnityEngine;

namespace Slaggy.Unity.Fields
{
    [Serializable]
    public struct Layer : IEquatable<Layer>, IComparable<Layer>
    {
        [SerializeField] private int _index;

        public int Mask => 1 << _index;

        public int Index
        {
            get => _index;
            set => _index = Mathf.Clamp(value, 0, 31);
        }

        public Layer(int index) => _index = index;

        public bool Equals(Layer other) => _index == other._index;
        public override bool Equals(object obj) => obj is Layer other && Equals(other);
        public override int GetHashCode() => _index * 293;
        public int CompareTo(Layer other) => _index.CompareTo(other._index);
    }
}