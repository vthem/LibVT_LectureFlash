using System;
using System.Collections.Generic;

namespace VT.Front
{
    public class FrontObjectComponentBinding : IDisposable
    {
        internal Func<FrontObjectComponent> ComponentConstructor { get; }

        private readonly string targetObjectName;

        public FrontObjectComponentBinding(string objectName, Func<FrontObjectComponent> constructor)
        {
            ComponentConstructor = constructor;
            targetObjectName = objectName;
            Registry.Objects.Added += ObjectAddedHandler;
            if (Registry.Objects.TryGet(objectName, out List<FrontObject> objs))
            {
                foreach (var obj in objs)
                {
                    ObjectAddedHandler(obj.FrontObjectName, obj);
                }
            }
        }

        private void ObjectAddedHandler(string id, FrontObject obj)
        {
            var component = ComponentConstructor();
            component.Initialize(obj);
            Registry.Components.Add(obj.Identifier, component);
        }

        public void Dispose()
        {
            Registry.Objects.Added -= ObjectAddedHandler;
            if (Registry.Objects.TryGet(targetObjectName, out List<FrontObject> objs))
            {
                foreach (var obj in objs)
                {
                    if (Registry.Components.TryGet(obj.Identifier, out List<FrontObjectComponent> components))
                    {
                        FrontSystem.Registry.RemoveComponentOfObject(obj);
                    }
                }
            }
        }
    }
}