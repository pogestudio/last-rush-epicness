using UnityEngine;
using System.Collections;

/*

This script takes care of all the health related stuff of the player.
Including taking damage, killing character, despawning character, and adding lives.

health - the starting of the player

*/

public class PlayerHealth : MonoBehaviour
{
	public int health = 3;
	
	private HealthBarLogic healthBar;

	void Start ()
	{
		//Find the gameobject with the healthbarlogic and assign at start.
		//It should be tagged with "HealthBar". 
		GameObject healthBarContainer = GameObject.FindGameObjectWithTag ("HealthBar");
		healthBar = healthBarContainer.GetComponent<HealthBarLogic> () as HealthBarLogic;
		if (!healthBar)
			Debug.LogError ("FAIL! Need a logic script for health bar!");
		healthBar.setLife (health); //set the initial life.
	
	}
	private void die ()
	{
		Debug.Log ("Player should die");
        //gameObject.SetActive (false);
        Destroy(gameObject);
	}

	public void applyDamage (int damage)
	{
		audio.Play ();
		health -= damage;
		healthBar.setLife (health);
		if (health <= 0) {
			die ();
		}
	}

}
