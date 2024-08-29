using Custom_scenes;
using Game;
using Interfaces;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Butttons
{
    public class Start_game : MonoBehaviour, ICallOnSceneChange
    {
        public Button button;
        // Start is called before the first frame update
        void Start()
        {
            Variable.ListToCallOnSceneChange ??= new System.Collections.Generic.List<ICallOnSceneChange>();
            Variable.ListToCallOnSceneChange.Add(this);
        }

        // Update is called once per frame
        void Update()
        {
            button.interactable = (NetworkManager.Singleton.IsHost);
            // /*  commented out for now || Variable.DictPlayersId.Count > 1*/);
        }
        
        
        public void StartGameOnClick()
        { 
            //TODO : if lobby leader
            ServerGameHandling.StartGameServerRpc();
        }

        public void OnSceneChange(int index) // On ne veut afficher ce bouton que pour l'hote du lobby, pour le moment mis sur IsClient car par encore de variable pour savoir si on est l'hote
        {
            gameObject.SetActive("Menu" == SceneManager.GetActiveScene().name);
        }
    }
}
