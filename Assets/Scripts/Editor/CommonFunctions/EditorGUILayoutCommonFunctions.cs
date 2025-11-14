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
            Undo.RegisterCompleteObjectUndo(scriptableObject, $"Saved scriptable object: {scriptableObject.name}");
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

        public static void TextFieldLabel(string title, int space, GUIStyle style, string textField)
        {
            DrawCentralLabel(title, space, style);
            textField = EditorGUILayout.TextField(textField);
        }

        public static void Vector3FieldLabel(string title, int space, GUIStyle style, ref Vector3 vector3Field)
        {
            DrawCentralLabel(title, space, style);
            vector3Field = EditorGUILayout.Vector3Field(string.Empty,vector3Field);
        }

        public static void ObjectFieldLabel<T>(string title, int space, GUIStyle style, T objectField, bool allowSceneObjects = false) where T : UnityEngine.Object
        {
            DrawCentralLabel(title, space, style);
            objectField = (T)EditorGUILayout.ObjectField(objectField, typeof(T), allowSceneObjects);
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

        public static void Vector3FieldTransformLabel(string title, int space, GUIStyle style, ref Vector3 vector3Field, Transform transformField, ref Vector3 neededTransformField)
        {
            ObjectFieldLabel<Transform>(title, space, style, transformField);

            if(transformField != null)
            {
                vector3Field = neededTransformField;
            }

            vector3Field = EditorGUILayout.Vector3Field(string.Empty, vector3Field);
        }

        public static void ScrollListButtonsWithCurrentField<T>(int scrollViewHeight, Color activeColor, ref Vector2 scrollPosition, List<T> list, T currentObject) where T : UnityEngine.Object
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(scrollViewHeight));

            for ( int i = 0; i < list.Count; i++ )
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

        public static bool LabelButtonField(int space, string labelText, string buttonText, int buttonWidth)
        {
            GUILayout.Space(space);

            GUILayout.BeginHorizontal();

            bool isPressed = GUILayout.Button(labelText, GUILayout.Width(buttonWidth));

            GUILayout.EndHorizontal();

            return isPressed;
        }

        public static void ToolBarTabs(int space, ref int currentTab, string[] tabNames)
        {
            GUILayout.Space(space);

            currentTab = GUILayout.Toolbar(currentTab, tabNames);
        }
    }
}

