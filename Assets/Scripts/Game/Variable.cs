using System.Collections.Generic;
using Interfaces;
using UnityEngine.SceneManagement;

namespace Game
{
    public static class Variable
    {
        public static List<ICallOnSceneChange> ListToCallOnSceneChange;
        public static Scene SceneCurrent => SceneManager.GetActiveScene();


    }
}