using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class HitHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    public ServerConnection servCo;
    private float _power;
    public float Power { 
        get => _power;
        set => _power = value > 100 ? _power = 100: value < 0.1 ? _power = 0 : value;
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!networkObject.IsOwner)
        {
            return;
        }
        //Debug.Log("Power: " + Power);
        if (Input.GetMouseButton(0))
            Power -= Input.GetAxis("Mouse Y");
        if (!Input.GetMouseButtonUp(0)) return; //TODO : qppeler la fonction cote serveur, pas ici
        Debug.Log(Power);
        HitServerRpc(gameObject.GetComponent<CamHandle>().AngleH, gameObject.GetComponent<CamHandle>().AngleV, Power);
        
    }
    
    [ServerRpc]
    public void HitServerRpc(float angleH, float angleV, float power)
    {
        if (!networkObject.IsOwner)
        {
            return;
        }
        ball.GetComponent<Rigidbody>().AddForce(
            new Vector3(
                Mathf.Cos(angleH * (Mathf.PI / 180)) *
                Mathf.Sin(angleV * (Mathf.PI / 180)) * power * -10,
                0,
                Mathf.Sin(angleH * (Mathf.PI / 180)) *
                Mathf.Sin(angleV * (Mathf.PI / 180)) * power * -10
            ), ForceMode.Impulse
        );
        servCo.BallHitServerRpc();
        Debug.Log($"{servCo.PlayerId}Ball hit");
    }
    
    
}
