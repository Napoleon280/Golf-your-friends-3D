using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Unity.Netcode;
using UnityEngine;

public class ListConnectedPlayers : NetworkBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Variable.DictPlayersId = new Dictionary<string, uint>();
    }

    private void OnConnectedToServer()
    {
        OnConnectedToServerRpc();
    }

    [ServerRpc]
    void OnConnectedToServerRpc()
    {
        ResetListClientRpc();
        foreach (var VARIABLE in Variable.DictPlayersId.Keys)
        {
            AddPlayerClientRpc(VARIABLE, Variable.DictPlayersId[VARIABLE]);
        }
    }
    
    [ClientRpc]
    public void ResetListClientRpc()
    {
        Variable.DictPlayersId = new Dictionary<string, uint>();
    }
    
    [ClientRpc]
    public void AddPlayerClientRpc(string playerName, uint id)
    {
        Variable.DictPlayersId[playerName] = id;
    }


    // Update is called once per frame
    void OnGUI()
    {
        Debug.Log("OnGUI ListConnectedPlayers.cs");
        GUILayout.BeginArea(new Rect(1500, 100, 100, 300));
        GUILayout.Label("Players");
        foreach (var VARIABLE in Variable.DictPlayersId.Keys)
        {
            GUILayout.Label(VARIABLE);
        }
        GUILayout.EndArea();
        
        GUILayout.BeginArea(new Rect(1601, 100, 50, 300));
        GUILayout.Label("Id");
        foreach (var VARIABLE in Variable.DictPlayersId.Keys)
        {
            GUILayout.Label(Variable.DictPlayersId[VARIABLE].ToString());
        }
        GUILayout.EndArea();
        
    }
}
