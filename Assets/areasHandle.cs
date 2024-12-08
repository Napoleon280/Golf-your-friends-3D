using System;
using Game.Spectator;
using Unity.Netcode;
using UnityEngine;

public class areasHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public SphereCollider sphereCollider;
    public Rigidbody rb;
    public CamHandle camHandle;
    
    
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
        Debug.Log("[SERVER]BallInHoleServerRpc");
        camHandle.isSpec.Value = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!networkObject.IsOwner) return;
        if (other.gameObject.CompareTag("NoCollideArea"))
        {
            NoCollideServerRpc();
        }
        if (other.gameObject.CompareTag("HoleArea"))
        {
            Debug.Log("InHoleArea");
            BallInHoleServerRpc();
            NoCollideServerRpc();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(!networkObject.IsOwner) return;
        if (other.gameObject.CompareTag("NoCollideArea")|| other.gameObject.CompareTag("HoleArea"))
        {
            ReCollideServerRpc();
        }
    }
    
    [ServerRpc]
    public void NoCollideServerRpc()
    {
        sphereCollider.excludeLayers = 0;
        rb.excludeLayers = LayerMask.GetMask("PlayersBalls");
        Debug.Log("InServerChangeArea "+ (LayerMask.GetMask("PlayersBalls")));
        //TODO : tt
    }
    
    [ServerRpc]
    public void ReCollideServerRpc()
    {
        sphereCollider.excludeLayers = 0;
        Debug.Log("OutServerChangeArea");
        //TODO : tt
    }
}
