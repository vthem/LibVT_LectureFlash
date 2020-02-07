using System.Collections.Generic;
using UnityEngine;
using VT.Collection;

namespace VT.Observer
{
    internal class ObserverRegistry
    {
        private readonly DictionaryList<string, IObserver> observers = new DictionaryList<string, IObserver>();
        private readonly VarRegistry varRegistry;

        internal ObserverRegistry(VarRegistry varRegistry)
        {
            this.varRegistry = varRegistry;
            this.varRegistry.VarAdded += NotifyObserver;
        }

        public void NotifyObserver(IObservable obj)
        {
            if (observers.TryGetList(obj.Name, out List<IObserver> observerList))
            {
                foreach (var observer in observerList)
                {
                    try
                    {
                        observer.VarUpdatedHandler(obj);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
        }

        public void Add(IObserver observer, bool notify = true)
        {
            observers.Add(observer.VarName, observer);
            if (notify && varRegistry.TryGetVar(observer.VarName, out IObservable obj))
            {
                NotifyObserver(obj);
            }
        }

        public void Remove(IObserver observer)
        {
            observers.Remove(observer.VarName, observer);
        }
    }
}