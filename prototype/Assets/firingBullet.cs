using UnityEngine;
using System.Collections;

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
            Debug.Log (Vector3.forward * speed);
            Destroy (newProjectile.gameObject, 10);
        }
    }

}
