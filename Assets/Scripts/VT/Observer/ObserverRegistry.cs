using System.Collections.Generic;
using UnityEngine;
using VT.Collection;

namespace VT.Observer
{
    internal static class ObserverRegistry
    {
        private static readonly DictionaryList<string, IObserver> observers = new DictionaryList<string, IObserver>();

        static ObserverRegistry()
        {
            VarRegistry.VarAdded += NotifyObserver;
        }

        public static void NotifyObserver(IObservable obj)
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

        public static void Add(IObserver observer)
        {
            Debug.Log($"Add observer {observer.Name} -> {observer.VarName}");
            observers.Add(observer.VarName, observer);
            if (VarRegistry.TryGetVar(observer.VarName, out IObservable obj))
            {
                NotifyObserver(obj);
            }
        }

        public static void Remove(IObserver observer)
        {
            observers.Remove(observer.VarName, observer);
        }
    }
}