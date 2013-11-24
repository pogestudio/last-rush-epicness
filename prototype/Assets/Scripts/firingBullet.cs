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

    public Rigidbody projectile;
    public int speed = 20;


    // Use this for initialization
    void Start ()
    {

	
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown (0)) {
            Rigidbody newProjectile = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
            newProjectile.velocity = transform.TransformDirection (Vector3.forward * speed);
			//Debug.Log(Vector3.forward * speed);
            Destroy (newProjectile.gameObject, 10);
        }
    }

}
