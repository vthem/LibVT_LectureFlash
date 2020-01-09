using VT.Observer;
using System;

namespace VT.Front
{
    public class FrontObjectModifier<Component, Var> : FrontObjectComponentBinding where Component : UnityEngine.Component
    {
        public FrontObjectModifier(string frontObjectName, string varName, Action<Component, Var> modify) : this(frontObjectName, () =>
        {
            return new FrontObjectModifierComponent<Component, Var>(frontObjectName, varName, modify);
        })
        {
        }

        internal FrontObjectModifier(string frontObjectName, Func<FrontObjectComponent> constructor) : base(frontObjectName, constructor)
        {
        }
    }

    internal class FrontObjectModifierComponent<Component, Var> : FrontObjectComponent where Component : UnityEngine.Component
    {
        private readonly GenericVarObserver<Var> observer;
        private readonly Action<Component, Var> modify;
        private Component component;
        private bool componentSet = false;

        internal FrontObjectModifierComponent(string frontObjectName, string varName, Action<Component, Var> modify) : base(frontObjectName)
        {
            observer = new GenericVarObserver<Var>(component.name, varName, VarUpdated);
            this.modify = modify;
        }

        private void VarUpdated(Var newValue)
        {
            if (!componentSet)
            {
                return;
            }
            modify?.Invoke(component, newValue);
        }

        protected override void InnerDispose()
        {
            observer?.Dispose();
        }

        protected override void InnerInitialize()
        {
            component = Object?.GetComponent<Component>();
            componentSet = component != null;
        }
    }
}