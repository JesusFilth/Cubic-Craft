using System;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class SerializeInterfaceAttribute : PropertyAttribute
    {
        public SerializeInterfaceAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
