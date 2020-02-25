using System;
using System.Collections.Generic;
using UnityEngine;

namespace VT.Observer
{
    internal class VarRegistry
    {
        private Dictionary<string, IObservable> vars = new Dictionary<string, IObservable>();

        public event Action<IObservable> VarAdded;

        public void Add(IObservable observable)
        {
            try
            {
                ObserverSystem.Logger.Debug($"Add var to registry Name={observable.Name} Type={observable.TypeString}");
                vars.Add(observable.Name, observable);
            }
            catch (ArgumentException ex)
            {
                ObserverSystem.Logger.Error($"IObservable {observable.Name} already exist {ex.Message}");
            }
            finally
            {
                VarAdded?.Invoke(observable);
            }
        }

        public void Remove(IObservable observable)
        {
            ObserverSystem.Logger.Debug($"Remove var from registry Name={observable.Name} Type={observable.TypeString}");
            vars.Remove(observable.Name);
        }

        public bool TryGetVar(string varName, out IObservable obj)
        {
            return vars.TryGetValue(varName, out obj);
        }
    }
}