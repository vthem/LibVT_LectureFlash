using System;
using UnityEngine.UI;
using VT.Messaging;
using UnityEngine;

namespace VT.Front
{
    public class Click : FrontObjectComponentBinding
    {
        public Click(string frontObjectName) : this(frontObjectName, () =>
        {
            return new ClickComponent(frontObjectName);
        })
        {
        }


        public Click(string objectName, Func<FrontObjectComponent> constructor) : base(objectName, constructor)
        {
        }
    }

    internal class ClickComponent : FrontObjectComponent
    {
        private Button button;

        internal ClickComponent(string frontObjectName) : base(frontObjectName)
        {
        }

        protected override void InnerDispose()
        {
            button.onClick.RemoveListener(OnClickHandler);
        }

        protected override void InnerInitialize()
        {
            button = Object.GetComponent<Button>();
            if (button == null)
            {
                Debug.Log($"no button found on {FrontObjectName}");
            }
            button.onClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            throw new System.Exception("broken");
            //var message = new Message { Name = "Click" }
            //    .Set("Source", FrontObjectName);
            //FrontAction.Dispatch(message);
        }
    }
}