using UnityEngine;

namespace VT.Front
{
    internal class FrontObjectFinder : MonoBehaviour
    {
        private void Awake()
        {
            var objs = gameObject.GetComponentsInChildren<FrontObject>();
            for (int i = 0; i < objs.Length; ++i)
            {
                objs[i].Register();
            }
        }
    }
}