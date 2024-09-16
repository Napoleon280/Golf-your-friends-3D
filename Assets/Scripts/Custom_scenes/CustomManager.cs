using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Custom_scenes
{
    public static class CustomManager
    {
        public static void ChangeScene(string newScene)
        {
            Debug.Log("Changing scene to " + newScene);
            
            Variable.SceneCurrent = newScene;
            
            SceneManager.LoadScene(newScene);
            Debug.Log("Calls : " + Variable.ListToCallOnSceneChange.Count);
            for (int i = Variable.ListToCallOnSceneChange.Count - 1; i >= 0; i--)
            { 
                Debug.Log("Calling OnSceneChange for " + i);
                Variable.ListToCallOnSceneChange[i].OnSceneChange(i);
            }
            Debug.Log("Scene changed to " + newScene);
        }
    }
}
