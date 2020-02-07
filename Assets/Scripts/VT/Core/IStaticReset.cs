using System.Collections.Generic;

namespace VT.Core
{
    public interface IStaticReset
    {
        void StaticReset();
    }

    public static class StaticResetRegistry
    {
        private static HashSet<IStaticReset> instances = new HashSet<IStaticReset>();

        public static void ResetAll()
        {
            foreach (var instance in instances)
            {
                instance.StaticReset();
            }
            instances.Clear();
        }

        public static void Add(IStaticReset instance)
        {
            instances.Add(instance);
        }
    }
}