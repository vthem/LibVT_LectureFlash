using System;
using System.Collections.Generic;
using UnityEngine;
using VT.Unity;

namespace VT.Front
{
    public class ScrollViewChildController : MonoBehaviour
    {
        private readonly List<GameObject> childs = new List<GameObject>();

        [SerializeField]
        private GameObject template = null;

        [SerializeField]
        private Transform container = null;

        public void Awake()
        {
            Debug.Log("WTF");
        }

        public void Sync(List<string> sync, Action<GameObject, string> objectAdded)
        {
            if (template == null)
            {
                FrontSystem.Logger.Error($"{transform.FullName()} not properly configured, template is null");
                return;
            }

            if (container == null)
            {
                FrontSystem.Logger.Error($"{transform.FullName()} not properly configured, container is null");
                return;
            }

            // remove childs not in elements
            var removed = new List<int>();
            for (int c = 0; c < childs.Count; ++c)
            {
                if (sync.IndexOf(childs[c].name) < 0)
                {
                    removed.Add(c);
                }
            }

            for (int c = 0; c < childs.Count; ++c)
            {
                GameObject.Destroy(childs[c]);
                childs.RemoveAt(c);
            }


            // find what needs to be addeed or removed
            var added = new List<int>();
            for (int s = 0; s < sync.Count; ++s)
            {
                int indexOf = -1;
                for (int c = 0; c < childs.Count; ++c)
                {
                    if (sync[s] == childs[c].name)
                    {
                        indexOf = s;
                        break;
                    }
                }
                if (indexOf < 0) // add the element
                {
                    var obj = Instantiate(template);
                    obj.transform.SetParent(container);
                    obj.transform.localScale = Vector3.one;
                    obj.SetActive(true);
                    obj.name = sync[s];
                    childs.Insert(s, obj);
                    objectAdded.Invoke(obj, sync[s]);
                }
                else if (indexOf != s) // move the element
                {
                    var obj = childs[indexOf];
                    childs.RemoveAt(indexOf);
                    childs.Insert(s, obj);
                }
            }
        }
    }
}