using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Editor.CommonFunctions.Layout;

namespace Editor.CommonFunctions.AssetDataBase
{
    public static class AssetDataBaseCommonFunctions
    {
        public static T CreateScriptableObject<T>(string scriptableObjectName, string folderPath) where T : ScriptableObject
        {
            if (!AssetDatabase.AssetPathExists(folderPath + '/' + scriptableObjectName + ".asset"))
            {
                T scriptableObject = ScriptableObject.CreateInstance<T>();
                scriptableObject.name = scriptableObjectName;

                AssetDatabase.CreateAsset(scriptableObject, folderPath + '/' + scriptableObject.name + ".asset");
                return scriptableObject;
            }
            else
            {
                return null;
            }
            
        }

        public static void CreatScriptableObjectAddToList<T>(string scriptableObjectName, string folderPath, List<T> list) where T : ScriptableObject 
        {
            T scriptableObject = CreateScriptableObject<T>(scriptableObjectName, folderPath);

            if (list != null)
            {
                if(scriptableObject != null)
                {
                    list.Add(scriptableObject);
                }
            }
        }

        public static T CreateScriptableObjectOnceOrFind<T>(string scriptableObjectName, string folderPath) where T : ScriptableObject
        {
            T scriptableObject = (T)AssetDatabase.LoadAssetAtPath( folderPath + "/" + scriptableObjectName + ".asset", typeof(T));

            if(scriptableObject == null)
            {
                scriptableObject = CreateScriptableObject<T>(scriptableObjectName, folderPath);
            }

            return scriptableObject;
        }

        public static bool RemoveScriptableObject<T>(T scriptableObject, string folderPath) where T : ScriptableObject
        {
            return AssetDatabase.DeleteAsset(folderPath + '/' + scriptableObject.name + ".asset");
        }

        public static bool RemoveScriptableObjectRemoveFromList<T>(T scriptableObject, string folderPath, List<T> list) where T : ScriptableObject
        {
            if (list != null)
            {
                list.Remove(scriptableObject);
            }
            else
            {
                return false;
            }

            return RemoveScriptableObject<T>(scriptableObject, folderPath);
        }

        public static bool RenameScriptableObject(ScriptableObject scriptableObject, string folderPath, string newName)
        {
            if (AssetDatabase.RenameAsset(folderPath + '/' + scriptableObject.name + ".asset", newName) == string.Empty)
            {
                scriptableObject.name = newName;
                EditorGUILayoutCommonFunctions.SaveScriptableObject(scriptableObject);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CreateFolder(string path, string folderName)
        {
            if (AssetDatabase.CreateFolder(path, folderName) != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CreateFolderOnce(string path, string folderName)
        {
            if (!AssetDatabase.IsValidFolder(path + '/' + folderName))
            {
                return CreateFolder(path, folderName);
            }
            else
            {
                return true;
            }
        }

        public static bool ScriptableObjectFolderToList<T>(string[] folderPaths, ref List<T> list) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}", folderPaths);

            list = new List<T>(guids.Length);

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                list.Add(AssetDatabase.LoadAssetAtPath<T>(path));
            }

            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

