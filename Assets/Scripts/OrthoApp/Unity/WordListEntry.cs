using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using VT.Unity;

namespace LectureFlash.Unity
{
    public class WordListEntry : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private TMP_Text textRef = null;

        public void SetWord(string word)
        {
            textRef.text = word;
        }

        public bool dragOnSurfaces = true;

        //private GameObject m_DraggingIcon;
        private RectTransform m_DraggingPlane;
        private Canvas canvas;

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            // We have clicked something that can be dragged.
            // What we want to do is create an icon for this.
            //m_DraggingIcon = new GameObject("icon");

            //m_DraggingIcon.transform.SetParent(canvas.transform, false);
            //m_DraggingIcon.transform.SetAsLastSibling();

            //var image = m_DraggingIcon.AddComponent<Image>();

            //image.sprite = GetComponent<Image>().sprite;
            //image.SetNativeSize();

            if (dragOnSurfaces)
                m_DraggingPlane = transform as RectTransform;
            else
                m_DraggingPlane = canvas.transform as RectTransform;

            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                var rt = GetComponent<RectTransform>();
                offset = rt.position - globalMousePos;
            }

            SetDraggedPosition(eventData);
        }

        Vector3 offset;

        public void OnDrag(PointerEventData data)
        {
            //if (m_DraggingIcon != null)


            SetDraggedPosition(data);
            transform.SetParent(canvas.transform);

        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

            var rt = GetComponent<RectTransform>();
            Debug.Log($"pos={data.position}");
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos + offset;
                rt.rotation = m_DraggingPlane.rotation;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //if (m_DraggingIcon != null)
            //    Destroy(m_DraggingIcon);
        }

        static public T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            Transform t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }

        string backup;

        void Update()
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition))
            {
                if (string.IsNullOrEmpty(backup))
                    backup = textRef.text;
                textRef.text = "in";
            }
            else if (!string.IsNullOrEmpty(backup))
            {
                textRef.text = backup;
                backup = string.Empty;
            }
        }
    }
}