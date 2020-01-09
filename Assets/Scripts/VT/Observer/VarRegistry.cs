using System;
using System.Collections.Generic;
using UnityEngine;

namespace VT.Observer
{
    public static class VarRegistry
    {
        private static Dictionary<string, IObservable> vars = new Dictionary<string, IObservable>();

        public static event Action<IObservable> VarAdded;

        public static void Add(IObservable observable)
        {
            Debug.Log($"Add observable {observable.Name}");
            try
            {
                vars.Add(observable.Name, observable);
            }
            catch (ArgumentException ex)
            {
                Debug.LogError($"IObservable {observable.Name} already exist {ex.Message}");
            }
            finally
            {
                VarAdded?.Invoke(observable);
            }
        }

        public static void Remove(IObservable observable)
        {
            vars.Remove(observable.Name);
        }

        public static bool TryGetVar(string varName, out IObservable obj)
        {
            return vars.TryGetValue(varName, out obj);
        }
    }
}