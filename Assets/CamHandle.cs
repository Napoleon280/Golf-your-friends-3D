using Unity.Netcode;
using UnityEngine;

public class CamHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    public float minDist;

    public float CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;
    public static float sAngleH { get; set; }
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
        CurrentDist += -Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButton(0)) { return; }
        AngleV -= Input.GetAxis("Mouse Y");
        sAngleH = AngleH;
    }
    

    public void LateUpdate()
    {
        Vector3 tmp;
        tmp.x = (Mathf.Cos(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + ball.position.x);
        tmp.z = (Mathf.Sin(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + ball.position.z);
        tmp.y = Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + ball.position.y;
        transform.position = Vector3.Slerp(transform.position, tmp, TranslateSpeed * Time.deltaTime);
        transform.LookAt(ball);
    }
}
