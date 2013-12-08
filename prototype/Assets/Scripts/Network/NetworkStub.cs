using UnityEngine;
using System.Collections;

public class NetworkStub : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //create stub server if NetworkManager don't exists
	    if(NetworkManager.offlineMode())
        {
            Network.InitializeServer(0, NetworkManager.PORT, false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
