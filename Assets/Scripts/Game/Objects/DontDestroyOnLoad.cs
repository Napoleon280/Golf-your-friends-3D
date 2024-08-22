using Custom_scenes;
using Interfaces;
using UnityEngine;

namespace Game.Objects
{
    public class DontDestroyOnLoad : MonoBehaviour, ICallOnSceneChange
    {
        private GameObject _gameObject;
    
        // Start is called before the first frame update
        void Start()
        {
            _gameObject = gameObject;
            DontDestroyOnLoad(_gameObject);
            if (Variable.ListToCallOnSceneChange is null) Variable.ListToCallOnSceneChange = new System.Collections.Generic.List<ICallOnSceneChange>();
            Variable.ListToCallOnSceneChange.Add(this);
        }

        public void OnSceneChange(int index)
        {
            if (Variable.SceneCurrent != Scenes.Menu) return;
        
            Variable.ListToCallOnSceneChange.RemoveAt(index);
            Destroy(_gameObject);
        }
    
    
    }
}