using System.Collections.Generic;
using Interfaces;
using UnityEngine.SceneManagement;

namespace Game
{
    public static class Variable
    {
        public static List<ICallOnSceneChange> ListToCallOnSceneChange;
        public static Scene SceneCurrent => SceneManager.GetActiveScene();

        public static Dictionary<uint, Dictionary<uint, uint>> DictPlayerScorePerHole;
        public static uint PlayerScoreHole(uint playerId, uint holeNumber)
        {
            return DictPlayerScorePerHole[playerId][holeNumber];
        }
        public static uint[] PlayerScoreHoles(uint playerId, uint holeNumber)
        {
            uint[] retour = new uint[] {};
            foreach (var VARIABLE in DictPlayerScorePerHole[playerId].Values)
            {
                retour[retour.Length] = VARIABLE;
            }
            return retour;
        }

        public static Dictionary<string, uint> DictPlayersId;

        public static uint CurrentHole;


    }
}