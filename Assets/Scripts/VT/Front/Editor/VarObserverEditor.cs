using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace VT.Front
{
    [CustomEditor(typeof(ComponentModifier))]
    public class VarObserverEditor : Editor
    {
        private SerializedProperty varNameProp;
        private SerializedProperty typeNameProp;
        private SerializedProperty setterNameProp;

        void OnEnable()
        {
            varNameProp = serializedObject.FindProperty("varName");
            typeNameProp = serializedObject.FindProperty("typeName");
            setterNameProp = serializedObject.FindProperty("setterName");
        }

        public override void OnInspectorGUI()
        {
            var comp = target as ComponentModifier;

            serializedObject.Update();
            EditorGUILayout.PropertyField(varNameProp);
            serializedObject.ApplyModifiedProperties();

            string buttonName = "Component / Setter";
            if (!string.IsNullOrEmpty(typeNameProp.stringValue)
                && !string.IsNullOrEmpty(setterNameProp.stringValue))
            {
                buttonName = $"{typeNameProp.stringValue}/{setterNameProp.stringValue}";
            }
            EditorGUILayout.LabelField("Target");

            if (GUILayout.Button(buttonName))
            {
                // create the menu and add items to it
                GenericMenu menu = new GenericMenu();

                foreach (var c in comp.GetComponents<Component>())
                {
                    foreach (var field in c.GetType().GetProperties(BindingFlags.SetField | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (!field.CanWrite)
                        {
                            continue;
                        }
                        menu.AddItem(new GUIContent($"{c.GetType().Name}/{field.Name}"), false, TargetSelectedHandler, new Tuple<Type, PropertyInfo>(c.GetType(), field));
                    }
                }

                // display the menu
                menu.ShowAsContext();
            }
        }

        private void TargetSelectedHandler(object param)
        {
            serializedObject.Update();

            var t = param as Tuple<Type, PropertyInfo>;
            typeNameProp.stringValue = t.Item1.Name;
            setterNameProp.stringValue = t.Item2.Name;
            serializedObject.ApplyModifiedProperties();
        }
    }
}