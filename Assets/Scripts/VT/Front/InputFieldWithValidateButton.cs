using UnityEngine;
using _InputField = TMPro.TMP_InputField;
using _Button = UnityEngine.UI.Button;
using VT.Unity;
using System;

namespace VT.Front
{
    [RequireComponent(typeof(FrontObject))]
    public class InputFieldWithValidateButton : MonoBehaviour
    {
        [SerializeField]
        private string messageName = string.Empty;

        [SerializeField]
        private _Button buttonRef = null;

        [SerializeField]
        private _InputField inputFieldRef = null;

        private FrontObject frontObject;

        // Start is called before the first frame update
        void Start()
        {
            inputFieldRef.onSubmit.AddListener(OnSubmitHandler);
            buttonRef.onClick.AddListener(SubmitValue);
            frontObject = GetComponent<FrontObject>();
            if (string.IsNullOrEmpty(messageName))
            {
                FrontSystem.Logger.Error($"[{transform.FullName()}] {nameof(messageName)} is null or empty");
            }
        }

        private void OnSubmitHandler(string v)
        {
            SubmitValue();
        }

        private void SubmitValue()
        {
            if (string.IsNullOrEmpty(inputFieldRef.text))
                return;

            var msg = new Messaging.Message();
            msg.Set("Source", frontObject.FrontObjectName);
            msg.Set("Type", "Submit");
            msg.Set("Value", inputFieldRef.text);
            msg.Name = messageName;
            FrontAction.Dispatch(msg);
        }

        public static FrontAction AddInputFieldSubmit(this FrontAction frontAction, Action<string> action)
        {
            frontAction.MessageHandler =
            return frontAction;
        }
    }
}