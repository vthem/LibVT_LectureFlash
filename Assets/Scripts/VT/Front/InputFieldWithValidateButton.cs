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
        }

        private void OnSubmitHandler(string v)
        {
            SubmitValue();
        }

        private void SubmitValue()
        {
            if (string.IsNullOrEmpty(inputFieldRef.text))
                return;

            FrontAction.Submit(frontObject.FrontObjectName, inputFieldRef.text);
        }
    }
}