using Slaggy.Unity.Fields;

using UnityEngine;

namespace Slaggy.Unity.Examples
{
    public class TypeBase { }
    public class TypeA : TypeBase { }
    public class TypeB : TypeBase { }
    public class TypeC : TypeB { }

    [System.Serializable] public class ConstrainedType : ConstrainedSerializableType<TypeBase> { }
    [System.Serializable] public class ConstrainedTypeB : ConstrainedSerializableType<TypeB> { }
    [System.Serializable] public class MonoBehaviourType : ConstrainedSerializableType<MonoBehaviour> { }

    public class ExampleSerializableTypeScript : MonoBehaviour
    {
        [Header("Simple Example Types")]
        public ConstrainedType constrainedType;
        public ConstrainedTypeB typesConstrainedToTypeB;

        [Header("All Types")]
        public SerializableType type;

        [Header("List Example")]
        public ConstrainedType[] constrainedTypes;

        [Header("MonoBehaviour Types")]
        public MonoBehaviourType mbType;
    }
}