using UnityEngine;
using System.Collections;

/*
Main mission of the script is to follow an object around as it moves.
The object of this component should NOT be a child object of the target.

Params
target - the transform it should be following
distance - the distance in z axis.
height  - the height of the Camera.

So the real distance would be (according to pythagoras) sqrt(distance^2+height^2).
*/

public class TopDownCamera : MonoBehaviour
{

	public Transform target;
	public float distance;
	public float height;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (target == null)
			return;

		float z = target.position.z - distance;
		float y = target.position.y + height;
		float x = target.position.x;

		transform.position = new Vector3 (x, y, z);

		transform.LookAt (target.position);

		RaycastHit[] hits;
		//Debug.Log ("transform.position = " + transform.position + ", target.position = " + target.position);
		//Debug.DrawRay (transform.position, transform.forward * 100);

		// For objects fading from last frame, reset them to fade back in
		GameObject[] fadingObjs = GameObject.FindGameObjectsWithTag("Fading");
		foreach (GameObject fadingObj in fadingObjs) {
			FadeOut fo = fadingObj.GetComponent<FadeOut>();
			if (fo)
				fo.endFade();
		}

		// However, all objects we're still hitting with the raycast need to keep fading
		hits = Physics.RaycastAll(transform.position - transform.forward * 10.0f, transform.forward, 1000.0f);
		for (int i = 0; i < hits.Length; i++) {
			if (!hits[i].transform.Equals(target)) {
				FadeOut fo = hits[i].collider.gameObject.GetComponent<FadeOut>();
				if (fo)
					fo.startFade();
			}
		}

		// Do the fade update for all fading objects
		fadingObjs = GameObject.FindGameObjectsWithTag("Fading");
		foreach (GameObject fadingObj in fadingObjs) {
			FadeOut fo = fadingObj.GetComponent<FadeOut>();
			if (fo)
				fo.doFade();
		}
    }
}


