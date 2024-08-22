using Game;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class CamHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    public float minDist;
    public static bool freeCam;

    public float CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;
    // Start is called before the first frame update
    void Start()
    {
        if (!networkObject.IsOwner)
        {
            Destroy(gameObject);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        AngleH += Input.GetAxis("Mouse X");
        CurrentDist += -Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButton(0)) { return; }
        AngleV -= Input.GetAxis("Mouse Y");
    }
    

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            freeCam = !freeCam;
        }

        if (freeCam)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position +=  transform.forward * (Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -=  transform.forward * (Time.deltaTime * 5);
            }
            /*if (Input.GetKey(KeyCode.D))
            {
                transform.position += (Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * (Time.deltaTime * 5);
            }*/
            transform.rotation = Quaternion.Euler(AngleV*10, AngleH*10, 0);
            
            return;
        }
        Vector3 tmp;
        tmp.x = (Mathf.Cos(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + ball.position.x);
        tmp.z = (Mathf.Sin(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + ball.position.z);
        tmp.y = Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + ball.position.y;
        transform.position = tmp;//Vector3.Slerp(transform.position, tmp, TranslateSpeed * Time.deltaTime);
        transform.LookAt(ball);
    }
}
