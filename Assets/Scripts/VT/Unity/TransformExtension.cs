using UnityEngine;

namespace VT.Unity
{
    public static class TransformExtension
    {
        public static string FullName(this Transform transform)
        {
            string fullName = transform.name;
            Transform cur = transform;
            while (cur.parent != null)
            {
                cur = cur.parent;
                fullName = $"{cur.name}/{fullName}";
            }
            return fullName;
        }
    }
}