using Custom_scenes;
using Game;
using Interfaces;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Butttons
{
    public class Start_game : MonoBehaviour, ICallOnSceneChange
    {
        // Start is called before the first frame update
        void Start()
        {
            Variable.ListToCallOnSceneChange ??= new System.Collections.Generic.List<ICallOnSceneChange>();
            Variable.ListToCallOnSceneChange.Add(this);
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnSceneChange(int index) // On ne veut afficher ce bouton que pour l'hote du lobby, pour le moment mis sur IsClient car par encore de variable pour savoir si on est l'hote
        {
            gameObject.SetActive(Scenes.Menu == SceneManager.GetActiveScene() && NetworkManager.Singleton.IsClient);
        }
    }
}
