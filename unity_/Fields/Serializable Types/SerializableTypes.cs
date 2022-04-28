using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slaggy.Unity.Fields
{
    [Serializable]
    public abstract class SerializableTypeBase
    {
        protected Type _type;
        public virtual Type type
        {
            get
            {
                if (_type != null && _oldAssemblyQualifiedName == assemblyQualifiedName)
                    return _type;

                _type = Type.GetType(assemblyQualifiedName);
                _oldAssemblyQualifiedName = assemblyQualifiedName;
                return _type;
            }
            set => InternalSetType(value);
        }

        private string _oldAssemblyQualifiedName = string.Empty;
        [SerializeField] protected string assemblyQualifiedName = string.Empty;

        public virtual string AssemblyQualifiedName
        {
            get => assemblyQualifiedName;
            set => assemblyQualifiedName = value;
        }

        protected SerializableTypeBase() { }
        protected SerializableTypeBase(string assemblyQualifiedName) : this(Type.GetType(assemblyQualifiedName)) { }

        protected SerializableTypeBase(Type type)
        {
            _type = type;
            _oldAssemblyQualifiedName = assemblyQualifiedName = type.AssemblyQualifiedName;
        }

        protected void InternalSetType(Type value)
        {
            _type = value;
            _oldAssemblyQualifiedName = assemblyQualifiedName = value.AssemblyQualifiedName;
        }

        public static implicit operator string(SerializableTypeBase serializableTypeBase)
            => serializableTypeBase.assemblyQualifiedName;
    }

    [Serializable]
    public abstract class ConstrainedSerializableTypeBase : SerializableTypeBase { }

    [Serializable]
    public abstract class ConstrainedSerializableType<TBaseConstraint> : ConstrainedSerializableTypeBase,
        IEquatable<ConstrainedSerializableType<TBaseConstraint>>, IEqualityComparer<ConstrainedSerializableType<TBaseConstraint>>
    {
        public override string AssemblyQualifiedName
        {
            get => base.AssemblyQualifiedName;
            set
            {
                Type newType = Type.GetType(value);
                if (newType != null && typeof(TBaseConstraint).IsAssignableFrom(newType))
                    base.AssemblyQualifiedName = value;
            }
        }

        public override Type type
        {
            get => base.type;
            set
            {
                if (value != null && typeof(TBaseConstraint).IsAssignableFrom(value))
                    type = value;
            }
        }

        public bool Equals(ConstrainedSerializableType<TBaseConstraint> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return type == other.type && assemblyQualifiedName == other.assemblyQualifiedName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConstrainedSerializableType<TBaseConstraint>)obj);
        }

        public bool Equals
            (
                ConstrainedSerializableType<TBaseConstraint> x,
                ConstrainedSerializableType<TBaseConstraint> y
            ) =>
            x != null && x.Equals(y);

        public override int GetHashCode()
        {
            unchecked
            {
                return ((type != null ? type.GetHashCode() : 0) * 353)
                       ^ (assemblyQualifiedName != null ? assemblyQualifiedName.GetHashCode() : 0);
            }
        }

        public int GetHashCode(ConstrainedSerializableType<TBaseConstraint> obj) => obj.GetHashCode();
        //public static implicit operator SerializableType<TBaseConstraint>(string text)
        //    => new SerializableType<TBaseConstraint>(text);
    }

    [Serializable]
    public sealed class SerializableType : SerializableTypeBase, IEquatable<SerializableType>, IEqualityComparer<SerializableType>
    {
        public SerializableType(Type type) : base(type) { }
        public SerializableType(string assemblyQualifiedName) : base(assemblyQualifiedName) { }

        public bool Equals(SerializableType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return type == other.type && assemblyQualifiedName == other.assemblyQualifiedName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SerializableType)obj);
        }

        public bool Equals(SerializableType x, SerializableType y) =>
            x != null && x.Equals(y);

        public override int GetHashCode()
        {
            unchecked
            {
                return ((type != null ? type.GetHashCode() : 0) * 397)
                       ^ (assemblyQualifiedName != null ? assemblyQualifiedName.GetHashCode() : 0);
            }
        }

        public int GetHashCode(SerializableType obj) => obj.GetHashCode();
    }
}