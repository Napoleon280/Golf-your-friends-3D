using Unity.Netcode;
using UnityEngine;

public class CamHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    public float minDist;

    public float CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;
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
        AngleH += Input.GetAxis("Mouse X");
        CurrentDist += -10*Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButton(0)) { return; }
        AngleV -= Input.GetAxis("Mouse Y");
    }
    

    public void LateUpdate()
    {
        Vector3 tmp = Quaternion.Euler(0,AngleH,AngleV) * (new Vector3(-CurrentDist, 0, 0)) + ball.position;
        transform.position = tmp;
        transform.LookAt(ball);
    }
}
