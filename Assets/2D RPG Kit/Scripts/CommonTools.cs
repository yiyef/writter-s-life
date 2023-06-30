using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

namespace Game.Scripts.GameEngine.Editor
{
    public class CommonTools
    {
        [MenuItem("writer/工具/清理空文件夹")]
        public static void RemoveAllEmptyDirectories()
        {
            RemoveAllEmptyDirectories(Application.dataPath);
        }


        public static void RemoveAllEmptyDirectories(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                if (Directory.GetDirectories(directory).Length != 0)
                    RemoveAllEmptyDirectories(directory);

                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, true);
                    AssetDatabase.Refresh();
                }
            }
        }

        [MenuItem("writer/Play Game", true)]
        static bool PlayGameValidate()
        {
            if (EditorApplication.isPlaying)
            {
                return false;
            }
            return true;
        }

        [MenuItem("writer/Play Game")]
        static bool PlayGame()
        {
            if (EditorApplication.isPlaying)
            {
                return false;
            }

            if (EditorSceneManager.GetActiveScene().name == "Startup")
            {
                EditorApplication.isPlaying = true;
            }
            else if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.sceneOpened += StartupOpenedCallBack;
                EditorSceneManager.OpenScene("Assets/Game/Startup.unity", OpenSceneMode.Single);
            }

            return true;
        }

        private static void StartupOpenedCallBack(Scene scene, OpenSceneMode mode)
        {
            EditorSceneManager.sceneOpened -= StartupOpenedCallBack;
            EditorApplication.isPlaying = true;
        }

        [MenuItem("writer/OpenPersistentDataPath")]
        public static void OpenPersistent()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

    }
}
