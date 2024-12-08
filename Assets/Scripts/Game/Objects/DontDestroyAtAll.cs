using Custom_scenes;
using Interfaces;
using UnityEngine;

namespace Game.Objects
{
    public class DontDestroyAtAll : MonoBehaviour
    {
        private GameObject _gameObject;
    
        // Start is called before the first frame update
        void Start()
        {
            _gameObject = gameObject;
            DontDestroyOnLoad(_gameObject);
        }
    }
}