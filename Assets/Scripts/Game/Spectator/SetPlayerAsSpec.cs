using Unity.Netcode;

public class SetPlayerAsSpec
{
    [ServerRpc]
    void SetSpecServerRpc(int playerId)
    {
        //TODO : dispawn player's ball, set player as freecam
    }
}
