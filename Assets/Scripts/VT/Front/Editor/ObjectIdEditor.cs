using System.Text;
using UnityEditor;
using UnityEngine;

namespace VT.Front
{
    [CustomEditor(typeof(ObjectIdEditor))]
    internal class ObjectIdEditor : Editor
    {
        [MenuItem("VT/Generate C# ObjectId")]
        public static void GenerateCSharpObjectId()
        {
            StringBuilder sb = new StringBuilder();
            var objs = Resources.FindObjectsOfTypeAll<FrontObject>();
            foreach (var obj in objs)
            {
                Debug.Log($"{obj.FrontObjectName}");
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

        }
    }
}