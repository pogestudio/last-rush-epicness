using UnityEngine;
using System.Collections;

public class PlayerReplica : MonoBehaviour {

    public const float PLAYER_CIRCLE_RADIUS = 300;

    bool initTag = false;
    Renderer mesh;
    GameObject mainPlayer;
    public Texture arrowTexture;

	// Use this for initialization
	void Start () {
        mesh = GetComponentInChildren<Renderer>();
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (!initTag)
        {
            initTag = true;
            if (networkView.isMine) //needs to be there: not working fine in Start or Awake
            {
                this.enabled = false;   //this script is only running on the replicas
                tag = "Player";
            }
            else
            {
                tag = "OtherPlayer";
            }
        }
	  
       

	}

    void OnGUI()
    {
         if (!mesh.isVisible)
        {
             Vector3 direction = transform.position - mainPlayer.transform.position;
             direction.Normalize();

             float x = Screen.width/2 + direction.x*PLAYER_CIRCLE_RADIUS - arrowTexture.width/2;
             float y = Screen.height/2 - direction.z*PLAYER_CIRCLE_RADIUS - arrowTexture.height/2;

             GUI.DrawTexture(new Rect(x, y, arrowTexture.width, arrowTexture.height), arrowTexture);
        }
    }
}
