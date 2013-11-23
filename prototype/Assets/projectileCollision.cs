using UnityEngine;
using System.Collections;

public class projectileCollision : MonoBehaviour
{

    public int damage = 0;

    // Use this for initialization
    void Start ()
    {
        if (damage == 0)
            Debug.Log ("Damage is 0 on a projectile, fix!");
    }
	
    void OnCollisionEnter (Collision collision)
    {
        GameObject target = collision.gameObject;
        target.transform.SendMessage ("applyDamage", damage, SendMessageOptions.DontRequireReceiver);
        

        Destroy (gameObject);
    }
}
