using Game;
using Unity.Netcode;
using UnityEngine;


public class CamHandle : NetworkBehaviour
{
    public NetworkObject networkObject;
    public Transform ball;
    public bool freeCam;
    public bool escFocus;

    public float CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!networkObject.IsOwner)
        {
            gameObject.GetComponent<Camera>().enabled = false;
            gameObject.GetComponent<AudioListener>().enabled = false;
            return;
        }
        freeCam = false;
        escFocus = false;
        Debug.Log("CamHandle Awake");
        if (Variable.SceneCurrent != "Map") return;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!networkObject.IsOwner)
        {
            return;
        }
        AngleH -= Input.GetAxis("Mouse X");
        CurrentDist += -10*Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButton(0)) { return; }
        AngleV -= Input.GetAxis("Mouse Y");
    }
    

    public void LateUpdate()
    {
        if (!networkObject.IsOwner)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escFocus = !escFocus;
            gameObject.GetComponent<Camera>().enabled = escFocus;
            gameObject.GetComponent<AudioListener>().enabled = escFocus;
        }


        if (escFocus) return;
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
            
        Vector3 tmp = Quaternion.Euler(0,-AngleH,AngleV) * (new Vector3(-CurrentDist, 0, 0)) + ball.position;
        transform.position = tmp;
        transform.LookAt(ball);
    }
}
