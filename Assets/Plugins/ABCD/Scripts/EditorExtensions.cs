using UnityEngine;
using UnityEditor;
using System.IO;

namespace ABCDExtensions
{
#if UNITY_EDITOR
    public class EditorExtensions
    {
        [MenuItem("Assets/Add Folder to .gitignore")]
        static void AddFolderToGitignore()
        {
            var ignoreFile = Path.Combine(Application.dataPath, @"..\", ".gitignore");
            Debug.Log($"Constructed path: {ignoreFile}");

            if (!File.Exists(ignoreFile))
            {
                Debug.LogError($"[Editor Extenstions] .gitignore file not found. Operation aborted");
                return;
            }

            var selection = Selection.activeObject;

            if (selection == null) return;

            var selectionPath = AssetDatabase.GetAssetPath(selection.GetInstanceID());

            if (selectionPath.Length < 0 || !Directory.Exists(selectionPath))
            {
                Debug.LogError("[Editor Extensions] Selected object is not a folder. Operation aborted");
                return;
            }

            using (var writer = new StreamWriter(ignoreFile, true))
            {
                writer.WriteLine($"{selectionPath}/");
                writer.WriteLine($"{selectionPath}*.*meta");

                Debug.Log($"Folder {selection.name} successfully added to .gitignore");
            }
        }
    }
#endif
}