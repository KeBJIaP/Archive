using System;

namespace Archive.Common.Containers.UnityContainers
{
    public class RegInfo
    {
        public Type Type { get; }
        public object Implementation { get; }

        public RegInfo(Type type, object implementation)
        {
            Type = type;
            Implementation = implementation;
        }

        public static RegInfo Create<T>(T implementation)
        {
            return new RegInfo(typeof(T), implementation);
        }
    }
}
