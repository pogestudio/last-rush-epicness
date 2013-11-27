using UnityEngine;
using System.Collections;

/*

This script will handle firing projectiles on mouse button down.
It's supposed to be added as a component to a weapon. 

Params:
projectile - the projectile which will be cloned during a shot.
This projectile should have mass: 0, and not be affectd by gravity

speed - the speed of the projectile. Keep lower than 50 to avoid
glitches in collision detection.

*/

public class firingBullet : MonoBehaviour
{
	private int speed;
	// Use this for initialization
	void Start ()
	{
		//player = GameObject.FindGameObjectWithTag ("Player");
	
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			GameObject projectile = ProjectileFactory.sharedFactory ().deliverProjectile (transform);
		}
	}
}
