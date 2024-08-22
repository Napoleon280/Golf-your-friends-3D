using UnityEngine.SceneManagement;

namespace Custom_scenes
{
    public static class Scenes
    {
        public static Scene Menu => SceneManager.GetSceneByName("Menu");
        public static Scene Map => SceneManager.GetSceneByName("Map");
    }
}
