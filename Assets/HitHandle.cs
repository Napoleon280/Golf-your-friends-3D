using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class HitHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    private float _power;
    public float Power { 
        get => _power;
        set => _power = value > 100 ? _power = 100: value < 0.1 ? _power = 0 : value;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (!networkObject.IsOwner)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Power: " + Power);
        if (Input.GetMouseButton(0))
            Power -= Input.GetAxis("Mouse Y");
        if (Input.GetMouseButtonUp(0))
        {//TODO : qppeler la fonction cote serveur, pas ici
            Debug.Log(Power);
            ball.GetComponent<Rigidbody>().AddForce(
                new Vector3(
                    Mathf.Cos(gameObject.GetComponent<CamHandle>().AngleH * (Mathf.PI / 180)) *
                    Mathf.Sin(gameObject.GetComponent<CamHandle>().AngleV * (Mathf.PI / 180)) * Power * -10,
                    0,
                    Mathf.Sin(gameObject.GetComponent<CamHandle>().AngleH * (Mathf.PI / 180)) *
                    Mathf.Sin(gameObject.GetComponent<CamHandle>().AngleV * (Mathf.PI / 180)) * Power * -10
                ), ForceMode.Impulse
            );
        }
    }
    
    
}
