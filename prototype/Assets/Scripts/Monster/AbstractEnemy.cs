using UnityEngine;
using System.Collections;

public class AbstractEnemy : MonoBehaviour
{

	public int health;
	public float movingSpeed;
	public GameObject target;
	public float searchRadius = 80F;
	
	private void die ()
	{
		//Debug.Log ("Monster should die");
		DropsLoot lootComponent = gameObject.GetComponent<DropsLoot> ();
		lootComponent.ShouldDropLoot ();
		Destroy (gameObject);
	}
	
	public void applyDamage (int damage)
	{
		health -= damage;
		if (health <= 0) {
			die ();
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		if (!target) {
			Debug.Log ("Does not have initial target"); 
		}
	}
	
	protected void walk ()
	{
		Vector3 delta = target.transform.position - transform.position;
		delta.Normalize ();
		float moveSpeed = movingSpeed * Time.deltaTime;
		transform.position = transform.position + (delta * moveSpeed);
		transform.LookAt (target.transform);
	}
}

