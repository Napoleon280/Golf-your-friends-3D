using Unity.Netcode;
using UnityEngine;

public class ClientHitHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    public ServerConnection servCo;
    private float _power;
    private Rigidbody _ballRb;
    private CamHandle _camHandle;
    public float Power { 
        get => _power;
        set => _power = value > 100 ? _power = 100: value < 0.1 ? _power = 0 : value;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!NetworkManager.Singleton.IsServer) return;
        _ballRb = ball.GetComponent<Rigidbody>();
        _camHandle = gameObject.GetComponent<CamHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!networkObject.IsOwner || _camHandle.freeCam || _camHandle.escFocus) return;
        
        if (Input.GetMouseButton(0))
            Power -= Input.GetAxis("Mouse Y");
        if (!Input.GetMouseButtonUp(0)) return;
        Debug.Log($"[CLIENT] player id ({servCo.PlayerId}) : sending ball hit with power {Power}");
        HitServerRpc(gameObject.GetComponent<CamHandle>().AngleH, Power);
        
    }
    
    [ServerRpc]
    public void HitServerRpc(float angleH, float power)
    {
        if (_ballRb.velocity.magnitude > 0.1f)
        {
            return;
        }
        
        _ballRb.AddForce(
            new Vector3(
                Mathf.Cos(angleH * (Mathf.PI / 180)) * power * 10,
                0,
                Mathf.Sin(angleH * (Mathf.PI / 180)) * power * 10
            ), ForceMode.Impulse
        );
        Debug.Log($"[SERVER] Ball hit, power {power}, made by :");
        servCo.BallHitServerRpc();
    }
    
    
}
