using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.CommonFunctions.Layout
{
    public static class EditorGUILayoutCommonFunctions
    {
        public static void SaveScriptableObject(ScriptableObject scriptableObject)
        {
            EditorUtility.SetDirty(scriptableObject);
        }

        public static void DrawCentralLabel(string title, int space, GUIStyle style)
        {
            GUILayout.Space(space);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label(title, style);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        public static void DrawCentralLabel(string title, int space, string side, char delimeter, GUIStyle style)
        {
            title = side + delimeter + title + delimeter + side;
            DrawCentralLabel(title, space, style);
        }

        public static string TextFieldLabel(string title, int space, GUIStyle style, string textField)
        {
            DrawCentralLabel(title, space, style);
            textField = EditorGUILayout.TextField(textField);

            return textField;
        }

        public static Vector3 Vector3FieldLabel(string title, int space, GUIStyle style, Vector3 vector3Field)
        {
            DrawCentralLabel(title, space, style);
            vector3Field = EditorGUILayout.Vector3Field(string.Empty, vector3Field);

            return vector3Field;
        }

        public static T ObjectFieldLabel<T>(string title, int space, GUIStyle style, T objectField, bool allowSceneObjects = false) where T : UnityEngine.Object
        {
            DrawCentralLabel(title, space, style);
            objectField = (T)EditorGUILayout.ObjectField(objectField, typeof(T), allowSceneObjects);

            return objectField;
        }

        public static void Ints2FieldLabel(int space, int labelWidth, int spaceBetween, string int1Text, string int2Text, ref int int1Field, ref int int2Field)
        {
            GUILayout.Space(space);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorGUIUtility.labelWidth = labelWidth;

            int1Field = EditorGUILayout.IntField(int1Text, int1Field);
            GUILayout.Space(spaceBetween);
            int2Field = EditorGUILayout.IntField(int2Text, int2Field);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static Transform Vector3FieldTransformLabel(string title, int space, GUIStyle style, ref Vector3 vector3Field, bool allowSceneObjects)
        {
            Transform transform = null;

            transform = ObjectFieldLabel<Transform>(title, space, style, transform, allowSceneObjects);

            if (transform != null)
            {
                return transform;
            }

            vector3Field = EditorGUILayout.Vector3Field(string.Empty, vector3Field);

            return transform;
        }

        public static void ScrollListButtonsWithCurrentField<T>(int scrollViewHeight, Color activeColor, ref Vector2 scrollPosition, ref List<T> list, ref T currentObject) where T : UnityEngine.Object
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(scrollViewHeight));

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == currentObject)
                {
                    GUI.backgroundColor = activeColor;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }

                if (GUILayout.Button(list[i].name))
                {
                    currentObject = list[i];
                }
            }

            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndScrollView();
        }

        public static T ScrollListButtons<T>(int scrollViewHeight, ref Vector2 scrollPosition, ref List<T> list) where T : UnityEngine.Object
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(scrollViewHeight));

            for (int i = 0; i < list.Count; i++)
            {
                if (GUILayout.Button(list[i].name))
                {
                    for (int k = i; k < list.Count; k++)
                    {
                        if (GUILayout.Button(list[i].name))
                        {
                        }
                    }

                    EditorGUILayout.EndScrollView();
                    return list[i];
                }
            }

            EditorGUILayout.EndScrollView();
            return null;
        }
        public static string ScrollListButtons(int scrollViewHeight, ref Vector2 scrollPosition, ref List<string> list)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(scrollViewHeight));

            for (int i = 0; i < list.Count; i++)
            {
                if (GUILayout.Button(list[i]))
                {
                    for (int k = i; k < list.Count; k++)
                    {
                        if (GUILayout.Button(list[i]))
                        {
                        }
                    }

                    EditorGUILayout.EndScrollView();
                    return list[i];
                }
            }
            EditorGUILayout.EndScrollView();
            return null;
        }

        public static bool LabelButtonField(int space, string labelText, string buttonText, int buttonWidth)
        {
            GUILayout.Space(space);

            GUILayout.BeginHorizontal();

            bool isPressed = GUILayout.Button(labelText, GUILayout.Width(buttonWidth));

            GUILayout.EndHorizontal();

            return isPressed;
        }

        public static int ToolBarTabs(int space, int currentTab, string[] tabNames)
        {
            GUILayout.Space(space);

            currentTab = GUILayout.Toolbar(currentTab, tabNames);

            return currentTab;
        }
    }
}

