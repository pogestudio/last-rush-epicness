using UnityEngine;
using System.Collections;

public class PowerupBounce : MonoBehaviour {

	public float speed;
	public float maxChange;
	public float midpoint;
	private float radians;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = midpoint + maxChange * Mathf.Sin (radians);
		transform.position = pos;

		radians = (radians + speed) % (2*Mathf.PI);
	}
}
