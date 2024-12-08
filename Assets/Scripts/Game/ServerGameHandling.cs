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
            Variable.DictPlayersIdServer = new Dictionary<string, uint>();
            Variable.DictPlayerScorePerHole = new Dictionary<uint, Dictionary<uint, uint>>();
            Variable.DictIdHoleFinished = new Dictionary<uint, bool>();
        }
        
        [ServerRpc]
        public static void StartGameServerRpc()
        {
            Variable.SendStartGame = true;
            Variable.CurrentHole = 1;
            foreach (var VARIABLE in Variable.DictPlayersIdServer.Values)
            {
                Variable.DictPlayerScorePerHole[VARIABLE] = new Dictionary<uint, uint>() {[Variable.CurrentHole] = 0};
                Variable.DictIdHoleFinished[Variable.CurrentHole] = false;
            }
        }
        
        
        [ServerRpc]
        public static void NextHoleServerRpc()
        {
            Variable.CurrentHole++;
            foreach (var VARIABLE in Variable.DictPlayersIdServer.Values)
            {
                Variable.DictPlayerScorePerHole[VARIABLE] = new Dictionary<uint, uint>() {[Variable.CurrentHole] = 0};
                Variable.DictIdHoleFinished[Variable.CurrentHole] = false;
                //reset position des joueurs au trou suivant
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