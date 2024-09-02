using System;
using System.Collections.Generic;
using Custom_scenes;
using Game;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class UpdatePlayersLobby : NetworkBehaviour
{
    public NetworkObject networkObject;

    private void Start()
    {
        if (!networkObject.IsOwner)
        {
            return;
        }
        Debug.Log("[CLIENT]OnConnectedToServer");
        OnConnectedToServerRpc();
    }

    [ServerRpc]
    void OnConnectedToServerRpc()
    {
        Debug.Log("[SERVER]OnConnectedToServerRpc");
        ResetListClientRpc();
        foreach (var VARIABLE in Variable.DictPlayersIdServer.Keys)
        {
            AddPlayerClientRpc(VARIABLE, Variable.DictPlayersIdServer[VARIABLE]);
        }
    }
    
    [ClientRpc]
    public void ResetListClientRpc()
    {
        Debug.Log("[CLIENT]ResetListClientRpc");
        Variable.DictPlayersIdClient = new Dictionary<string, uint>();
    }
    
    [ClientRpc]
    public void AddPlayerClientRpc(string playerName, uint id)
    {
        Debug.Log("[CLIENT]AddPlayerClientRpc");
        Variable.DictPlayersIdClient[playerName] = id;
    }

    public void Update()
    {
        if (!Variable.SendStartGame) return;
        StartGameClientRpc();
        Variable.SendStartGame = false;
    }


    [ClientRpc]
    public void StartGameClientRpc()
    {
        Debug.Log("[CLIENT]Client received StartGameRpc.");
        CustomManager.ChangeScene("Map");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
