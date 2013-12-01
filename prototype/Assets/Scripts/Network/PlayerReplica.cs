using UnityEngine;
using System.Collections;

public class PlayerReplica : MonoBehaviour {

    private NetworkView view;

	// Use this for initialization
	void Start () {
        view = GetComponent<NetworkView>();
        if (!view)
        {
            Debug.Log("A network view should be in the same object");
        }
        else
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Should be in Start() but known unity bug that makes it
        if (view.isMine)
        {
            Debug.Log("HAHA");
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            Debug.Log("HOHO");

        }
	}
}
