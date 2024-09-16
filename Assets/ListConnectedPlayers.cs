using System.Collections.Generic;
using Game;
using UnityEngine;

public class ListConnectedPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Variable.DictPlayersIdClient = new Dictionary<string, uint>();
    }
    
    // Update is called once per frame
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(1500, 100, 100, 300));
        GUILayout.Label("Players");
        foreach (var VARIABLE in Variable.DictPlayersIdClient.Keys)
        {
            GUILayout.Label(VARIABLE);
        }
        GUILayout.EndArea();
        
        GUILayout.BeginArea(new Rect(1601, 100, 50, 300));
        GUILayout.Label("Id");
        foreach (var VARIABLE in Variable.DictPlayersIdClient.Keys)
        {
            GUILayout.Label(Variable.DictPlayersIdClient[VARIABLE].ToString());
        }
        GUILayout.EndArea();
        
    }
}
