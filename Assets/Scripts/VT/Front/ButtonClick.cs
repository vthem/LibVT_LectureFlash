using UnityEngine;

namespace VT.Front
{
    [RequireComponent(typeof(UnityEngine.UI.Button), typeof(FrontObject))]
    public class ButtonClick : MonoBehaviour
    {
        [SerializeField]
        private string messageName = string.Empty;

        private UnityEngine.UI.Button buttonRef;
        private FrontObject frontObject;

        // Start is called before the first frame update
        void Start()
        {
            buttonRef = GetComponent<UnityEngine.UI.Button>();
            buttonRef.onClick.AddListener(ClickHandler);
            frontObject = GetComponent<FrontObject>();
        }

        private void ClickHandler()
        {
            throw new System.Exception("broken");
            //var msg = new Messaging.Message();
            //msg.Set("Source", frontObject.FrontObjectName);
            //msg.Set("Type", "Click");
            //msg.Name = messageName;
            //FrontAction.Dispatch(msg);
        }
    }
}