using System;
using System.Collections.Generic;
using Game;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class ServerConnection : NetworkBehaviour
{
    public static string PlayerName = "";
    public NetworkObject networkObj;

    public static uint? PlayerId;
    // Start is called before the first frame update
    void Start()
    {
        if (!networkObj.IsOwner)
        {
            return;
        }
        AddPlayerServerRpc(PlayerName);
    }
    
    [ClientRpc]
    public void SetPlayerIdClientRpc(uint id)
    {
        if (!networkObj.IsOwner)
        {
            return;
        }
        PlayerId ??= id;
    }
    
    [ClientRpc]
    public void NameTakenOrInvalidClientRpc()
    {
        if (!networkObj.IsOwner)
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
