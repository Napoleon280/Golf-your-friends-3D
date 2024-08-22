using System;
using System.Collections.Generic;
using Game;
using Unity.Netcode;
using UnityEngine;

public class ServerConnection : NetworkBehaviour
{
    public static string PlayerName = "";
    private static NetworkObject _networkObject;

    public static uint? PlayerId;
    // Start is called before the first frame update
    void Start()
    {
        if (!_networkObject.IsOwner)
        {
            return;
        }
        AddPlayerServerRpc(PlayerName);
    }
    
    [ClientRpc]
    public void SetPlayerIdClientRpc(uint id)
    {
        if (!_networkObject.IsOwner)
        {
            return;
        }
        PlayerId ??= id;
    }
    
    [ClientRpc]
    public void NameTakenOrInvalidClientRpc()
    {
        if (!_networkObject.IsOwner)
        {
            return;
        }
        if (PlayerId is not null) return;
        PlayerName = "";
        NetworkManager.Singleton.Shutdown();
    }
    
    [ServerRpc]
    public void AddPlayerServerRpc(string playerName)
    {
        if (playerName== "" || Variable.DictPlayersId.ContainsKey(playerName))
        {
            NameTakenOrInvalidClientRpc();
            return;
        }
        uint id = (uint)Variable.DictPlayersId.Count;
        Variable.DictPlayersId[playerName] = id;
        Variable.DictPlayerScorePerHole[id] = new Dictionary<uint, uint>() {[Variable.CurrentHole] = 0};
        SetPlayerIdClientRpc(id);
    }
    
    [ServerRpc]
    public void BallHitServerRpc()
    {
        Variable.DictPlayerScorePerHole[(uint)PlayerId!][Variable.CurrentHole] += 1;
        Debug.Log("[SERVER]Player " + (uint)PlayerId + " score: " + Variable.DictPlayerScorePerHole[(uint)PlayerId][Variable.CurrentHole]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
