using System;
using System.Reflection;
using UnityEngine;
using VT.Unity;

namespace VT.Front
{
    public class ComponentModifier : MonoBehaviour
    {
        [SerializeField]
        private string typeName = string.Empty;

        [SerializeField]
        private string setterName = string.Empty;

        [SerializeField]
        private string varName = string.Empty;

        private Component targetComponent;
        private PropertyInfo targetProperty;
        private Observer.VarObserver observer;
        private MethodInfo targetMethod;
        private readonly object[] invokeParams = new object[1];

        private void Start()
        {
            targetComponent = GetComponent(typeName);
            targetProperty = targetComponent.GetType().GetProperty(setterName);
            targetMethod = targetProperty.GetSetMethod();
            observer = new Observer.VarObserver(transform.FullName(), varName, VarUpdatedHandler);
        }

        private void VarUpdatedHandler(object newValue)
        {
            invokeParams[0] = Convert.ChangeType(newValue, targetMethod.GetParameters()[0].ParameterType);
            targetMethod.Invoke(targetComponent, invokeParams);
        }

        private void OnDestroy()
        {
            observer?.Dispose();
        }
    }
}