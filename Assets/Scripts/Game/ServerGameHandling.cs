using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Game
{
    public static class ServerGameHandling
    {
        [ServerRpc]
        public static void Initializing()
        {
            Debug.Log("[SERVER]Initializing");
            Variable.DictPlayersId = new Dictionary<string, uint>();
            Variable.DictPlayerScorePerHole = new Dictionary<uint, Dictionary<uint, uint>>();
            Debug.Log("[SERVER]Initialized");
        }
        
        [ServerRpc]
        public static void StartGameServerRpc()
        {
            Variable.CurrentHole = 1;
            foreach (var VARIABLE in Variable.DictPlayersId.Values)
            {
                Variable.DictPlayerScorePerHole[VARIABLE] = new Dictionary<uint, uint>() {[Variable.CurrentHole] = 0};
            }
        }
        
        [ServerRpc]
        public static void NextHoleServerRpc()
        {
            Variable.CurrentHole++;
            foreach (var VARIABLE in Variable.DictPlayersId.Values)
            {
                Variable.DictPlayerScorePerHole[VARIABLE] = new Dictionary<uint, uint>() {[Variable.CurrentHole] = 0};
            }
        }
        
        [ServerRpc]
        public static void BallHitServerRpc(uint playerId)
        {
            Variable.DictPlayerScorePerHole[playerId][Variable.CurrentHole] += 1;
            Debug.Log("[SERVER]Player " + playerId + " score: " + Variable.DictPlayerScorePerHole[playerId][Variable.CurrentHole]);
        }
        
    }
}