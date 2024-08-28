using System.Collections.Generic;
using Custom_scenes;
using Game;
using Unity.Netcode;
using UnityEngine;

public class ServerConnection : NetworkBehaviour
{
    public static string PlayerName = "";
    public NetworkObject networkObj;

    public uint? PlayerId;
    // Start is called before the first frame update
    void Start()
    {
        if (!networkObj.IsOwner)
        {
            return;
        }
        Debug.Log("Hey Im the player prefab");
        AddPlayerServerRpc(PlayerName);
    }
    
    [ClientRpc]
    public void SetPlayerIdClientRpc(uint id)
    {
        Debug.Log("[CLIENT]SetPlayerIdClientRpc");
        if (!networkObj.IsOwner)
        {
            return;
        }
        PlayerId ??= id;
        Debug.Log("[CLIENT]PlayerId: " + PlayerId);
    }
    
    [ClientRpc]
    public void NameTakenOrInvalidClientRpc()
    {
        if (!networkObj.IsOwner)
        {
            return;
        }
        if (PlayerId is not null) return;
        Debug.LogError("[CLIENT]NameTakenOrInvalidClientRpc : Ce nom est deja pris ou invalide");
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
        PlayerId = id;
        Variable.DictPlayersId[playerName] = id;
        Variable.DictPlayerScorePerHole[id] = new Dictionary<uint, uint>() {[Variable.CurrentHole] = 0};
        SetPlayerIdClientRpc(id);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void BallHitServerRpc()
    {
        Variable.DictPlayerScorePerHole[(uint)PlayerId!][Variable.CurrentHole] += 1;
        Debug.Log("[SERVER]Player " + (uint)PlayerId + " score: " + Variable.DictPlayerScorePerHole[(uint)PlayerId][Variable.CurrentHole]);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void ResetServerRpc()
    {
        Variable.DictPlayerScorePerHole = new Dictionary<uint, Dictionary<uint, uint>>();;
        Debug.Log("[SERVER]DictPlayerScorePerHole resetted.");
    }
    
    [ClientRpc]
    public void ResetClientRpc()
    {
        if (!networkObj.IsOwner)
        {
            return;
        }
        CustomManager.ChangeScene("Menu");
        Debug.Log("[CLIENT]Client resetted.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
