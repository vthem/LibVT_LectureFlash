using System;

namespace VT.Front
{
    public abstract class FrontObjectComponent : IDisposable
    {
        public string FrontObjectName { get; }
        internal FrontObject Object { get; private set; }

        public FrontObjectComponent(string frontObjectName)
        {
            FrontObjectName = frontObjectName;
        }

        internal void Initialize(FrontObject obj)
        {
            Object = obj;
            InnerInitialize();
        }

        protected abstract void InnerInitialize();
        protected abstract void InnerDispose();

        public void Dispose()
        {
            InnerDispose();
        }
    }
}