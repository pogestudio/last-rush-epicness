using UnityEngine;
using System.Collections;

public class DemoClipCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 pos = transform.position;
		//pos.x += Time.deltaTime * 1.5f;

		transform.position = pos;*/

		Vector3 pos = transform.position;
		pos.x += Time.deltaTime * transform.forward.x * 0.3f;
		pos.y += Time.deltaTime * transform.forward.y * 0.3f;
		pos.z += Time.deltaTime * transform.forward.z * 0.3f;
		transform.position = pos;
	}
}
