using Game;
using Interfaces;
using Unity.Netcode;


public class EnableChildsOnMap : NetworkBehaviour, ICallOnSceneChange
{
    public NetworkObject networkObject;
    
    // Start is called before the first frame update
    void Start()
    {
        OnSceneChange(0);
        if (!networkObject.IsOwner)
        {
            return;
        }
        Variable.ListToCallOnSceneChange ??= new System.Collections.Generic.List<ICallOnSceneChange>();
        Variable.ListToCallOnSceneChange.Add(this);
        
    }
    
    public void OnSceneChange(int i)
    {
        //Debug.Log("OnSceneChange to enable childs (" + transform.childCount + " childs)");
        for (int j = transform.childCount - 1; j >= 0; j--)
        {
            //Debug.Log("Child " + j + " is active : " + (Variable.SceneCurrent == "Map"));
            transform.GetChild(j).gameObject.SetActive(Variable.SceneCurrent == "Map");
        }
        
    }
}
