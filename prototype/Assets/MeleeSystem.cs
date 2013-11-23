using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	public int damage;
	public float range;
	private float nextFire = 0.0F;
	private float fireRate = 1.0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray targetHitRay = new Ray (transform.position, transform.TransformDirection(Vector3.forward));
		if (Physics.Raycast(targetHitRay, out hit, range))
		{
			//Debug.Log("SOMETHING hit");
			if (hit.collider.tag == "Player" && Time.time > nextFire)
			{
				hit.transform.SendMessage("applyDamage",damage,SendMessageOptions.DontRequireReceiver);
				Debug.Log("log HIT HIT HIT");
				nextFire = Time.time + fireRate;
			}
		}
	}
}
