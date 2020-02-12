using System;
using System.Collections.Generic;
using VT.Collection;

namespace VT.Front
{
    public class FrontAction : IDisposable
    {
        private static DictionaryList<string, FrontAction> actionsByTarget = new DictionaryList<string, FrontAction>();
        private readonly string targetFrontObject;
        private Action<string> submitCallback;

        public FrontAction(string targetFrontObject)
        {
            this.targetFrontObject = targetFrontObject;
            actionsByTarget.Add(targetFrontObject, this);
        }

        public void Dispose()
        {
            actionsByTarget.Remove(targetFrontObject, this);
        }

        public FrontAction OnSubmit(Action<string> callback)
        {
            submitCallback = callback;
            return this;
        }

        internal static void Submit(string source, string value)
        {
            FrontSystem.Logger.Debug($"Submit {source} {value}");
            if (!actionsByTarget.TryGetList(source, out List<FrontAction> actions))
            {
                return;
            }
            for (int i = 0; i < actions.Count; ++i)
            {
                try
                {
                    actions[i]?.submitCallback(value);

                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogException(ex);
                }
            }
        }
    }
}