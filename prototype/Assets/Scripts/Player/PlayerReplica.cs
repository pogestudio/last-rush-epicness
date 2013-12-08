using UnityEngine;
using System.Collections;

public class PlayerReplica : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (networkView.isMine)
        {
            tag = "Player";
        }
        else
        {
            tag = "OtherPlayer";
        }

        this.enabled = false;
	}
}
