using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Custom_scenes
{
    public static class CustomManager
    {
        public static void ChangeScene(string newScene)
        {
            SceneManager.LoadScene(newScene);
            /*for (int i = 0; i < Variable.ListToCallOnSceneChange.Count; i++)
            { 
                Variable.ListToCallOnSceneChange[i].OnSceneChange(i);
            }*/
        }
    }
}
