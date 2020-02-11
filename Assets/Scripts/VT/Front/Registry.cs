
using System.Collections.Generic;

namespace VT.Front
{
    internal class Registry
    {
        public static Collection.Registry<string, FrontObject> Objects { get; } = new Collection.Registry<string, FrontObject>();
        public static Collection.Registry<string, FrontObjectComponent> Components { get; } = new Collection.Registry<string, FrontObjectComponent>();

        internal Registry()
        {
            Objects.Removed += ViewObjectRemovedHandler;
        }

        private void ViewObjectRemovedHandler(string id, FrontObject obj)
        {
            RemoveComponentOfObject(obj);
        }

        public void RemoveComponentOfObject(FrontObject obj)
        {
            if (Components.TryGet(obj.Identifier, out List<FrontObjectComponent> components))
            {
                var tmpList = new List<FrontObjectComponent>(components);
                foreach (var comp in tmpList)
                {
                    comp.Dispose();
                    Components.Remove(obj.Identifier, comp);
                }
            }
        }
    }
}