using UnityEngine;
using _InputField = TMPro.TMP_InputField;
using _Button = UnityEngine.UI.Button;
using _Logger = VT.Core.Logger;

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

        private _Logger logger = new _Logger(nameof(InputFieldWithValidateButton));

        // Start is called before the first frame update
        void Start()
        {
            inputFieldRef.onEndEdit.AddListener(EndEditHandler);
            buttonRef.onClick.AddListener(Validate);
            frontObject = GetComponent<FrontObject>();
        }

        private void EndEditHandler(string v)
        {
            Validate();
        }

        private void Validate()
        {
            logger.Trace();

            var msg = new Messaging.Message();
            msg.Set("Source", frontObject.FrontObjectName);
            msg.Set("Type", "Click");
            msg.Set("Value", inputFieldRef.text);
            msg.Name = messageName;
            FrontAction.Dispatch(msg);
        }
    }
}