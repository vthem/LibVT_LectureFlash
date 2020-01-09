using UnityEngine;
using VT.Unity;

namespace VT.Front
{
    public class ListObjectCreator : MonoBehaviour
    {
        [SerializeField]
        private string varName = string.Empty;

        [SerializeField]
        private GameObject template = null;

        [SerializeField]
        private Transform parent = null;

        private Observer.GenericVarObserver<string[]> addedObserver;

        // Start is called before the first frame update
        void Start()
        {
            addedObserver = new Observer.GenericVarObserver<string[]>(transform.FullName(), $"{varName}.Added", AddedHandler);
        }

        private void AddedHandler(string[] adds)
        {
            foreach (var add in adds)
            {
                Debug.Log($"***{add}***");
            }
        }

        protected void OnDestroy()
        {
            addedObserver?.Dispose();
        }
    }
}