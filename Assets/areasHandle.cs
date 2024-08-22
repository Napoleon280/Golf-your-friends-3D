using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class areasHandle : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [ServerRpc]
    public void BallInHoleServerRpc()
    {
        Debug.Log("ServerChangeArea");
        //TODO : tt
    }
}
