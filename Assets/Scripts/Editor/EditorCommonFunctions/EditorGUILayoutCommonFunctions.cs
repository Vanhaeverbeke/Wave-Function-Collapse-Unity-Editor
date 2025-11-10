using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.MessageBox;

namespace Editor.CommonFunctions.EditorGUILayout
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


    }
}

