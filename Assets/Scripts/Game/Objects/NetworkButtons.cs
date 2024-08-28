using Custom_scenes;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Game;
using UnityEngine.SceneManagement;

namespace Game.Objects
{
    /// <summary>
    /// Classe gerant les boutons lies au multijoueur
    /// </summary>
    public class Show : MonoBehaviour
    {
        public UnityTransport transport;
        private string _ip = "";
        private uint _compte;
        private string _textButton = "Connect";

        public void Start()
        {
            transport = GameObject.Find("Multi_object").GetComponent<UnityTransport>();
        }

        private void OnGUI()
        {
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            // Si on est pas client ni host
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
            {
                // Acces au bouton pour host 
                

                // Si on est dans le menu, on a acces au bouton pour etre client
                if (Variable.SceneCurrent == "Menu")
                {
                    if (GUILayout.Button("Host"))
                    {
                        //Debug.Log(Scenes.Map);
                        ServerGameHandling.Initializing();
                        Debug.Log(NetworkManager.Singleton.StartHost());
                    }
                    
                    GUILayout.TextField("Pseudo");
                    ServerConnection.PlayerName = GUILayout.TextField(ServerConnection.PlayerName, new []{GUILayout.Width(200)});
                    GUILayout.TextField("IP");
                    _ip = GUILayout.TextField(_ip, new []{GUILayout.Width(200)});
                    transport.ConnectionData.Address = _ip;
                    if (GUILayout.Button(_textButton))
                    {
                        if (_textButton == "Invalid IPv4" && _compte > 500) { _textButton = "Connect"; _compte = 0; }
                        else if (_textButton == "Invalid IPv4") { _compte++; }
                        
                        //if (!_ip.IsIpv4()) { _textButton = "Invalid IPv4";}
                        
                        NetworkManager.Singleton.StartClient();
                    }
                }
            }
            else
            {
                // Si on est host
                if (NetworkManager.Singleton.IsHost)
                {
                    // Affichage des informations du serveur
                    GUILayout.Label("Server");
                    GUILayout.Label($"IP : {NetworkManager.Singleton.ConnectedHostname}");
                    GUILayout.Label($"Connected : {NetworkManager.Singleton.ConnectedClients.Count}");

                    // Bouton pour arreter le serveur
                    if (GUILayout.Button("Menu"))
                    {
                        //TODO : reset game (dico des coups et reset position des player), change scene to menu
                        //NetworkManager.Singleton.Shutdown();
                        // oldTODO : savegame
                        //ChangeScene("Menu");
                    }
                }
                // Si on est client
                else if (NetworkManager.Singleton.IsClient)
                {
                    // Affichage des informations du client
                    GUILayout.Label("Client");
                    GUILayout.Label($"ID: {NetworkManager.Singleton.LocalClientId}");

                    // Bouton pour se deconnecter
                    if (GUILayout.Button("Disconnect"))
                    {
                        NetworkManager.Singleton.Shutdown();
                        CustomManager.ChangeScene("Menu");
                    }
                }

            }

            GUILayout.EndArea();

        }
    }
}