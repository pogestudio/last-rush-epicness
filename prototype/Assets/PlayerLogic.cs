using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {

	public int health = 3;

	private void die()
	{
		Debug.Log ("Player should die");
		gameObject.SetActive (false);
	}

	public void applyDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			die();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
