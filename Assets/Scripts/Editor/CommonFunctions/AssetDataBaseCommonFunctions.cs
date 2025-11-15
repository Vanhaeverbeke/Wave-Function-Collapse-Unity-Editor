using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Editor.CommonFunctions.AssetDataBase
{
    public static class AssetDataBaseCommonFunctions
    {
        public static bool CreateScriptableObject<T>(T scriptableObject, string folderPath) where T : ScriptableObject
        {
            if (!AssetDatabase.AssetPathExists(folderPath + '/' + scriptableObject.name + ".asset"))
            {
                AssetDatabase.CreateAsset(scriptableObject, folderPath + '/' + scriptableObject.name + ".asset");
                
                if(AssetDatabase.AssetPathExists(folderPath + '/' + scriptableObject.name + ".asset"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public static bool CreatScriptableObjectAddToList<T>(T scriptableObject, string folderPath, List<T> list) where T : ScriptableObject 
        {
            if(list != null)
            {
                list.Add(scriptableObject);
            }
            else
            {
                return false;
            }

            return CreateScriptableObject<T>(scriptableObject, folderPath);
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

