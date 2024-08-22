using Unity.Netcode;

namespace Game.Spectator
{
    public class SetPlayerAsSpec
    {
        [ServerRpc]
        void SetSpecServerRpc(int playerId)
        {
            //TODO : dispawn player's ball (via object sync), set player as freecam (via RPC)
        }
        
        [ClientRpc]
        void SetSpecClientRpc(int playerId)
        {
            //TODO : set player's cam as freecam (enable movement, disable ball looking, disable freetime limit?, )
        }
        
    }
}
