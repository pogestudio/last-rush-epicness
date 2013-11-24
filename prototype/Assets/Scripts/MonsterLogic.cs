using UnityEngine;
using System.Collections;

/*

This is monster logic, and decides all monster related behaviour.
health - the health of the monster

movingSpeed - decides how fast the monster is moving


GUIDING SHOULD BE REFACTORED to a seperate component. 

*/

public class MonsterLogic : MonoBehaviour
{

	public int health = 50;
	public float movingSpeed;
	public GameObject player;

	private void die ()
	{
		Debug.Log ("Monster should die");
		gameObject.SetActive (false);
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
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player"); 
			if (!player) 
				Debug.Log ("ERROR could not find Player!"); 
		}
		
            
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!player) 
			return; 


		//WALKING FUNCTION
		//float distance = Vector3.Distance( player.transform.position, transform.position);
		Vector3 delta = player.transform.position - transform.position;
		delta.Normalize ();
		float moveSpeed = movingSpeed * Time.deltaTime;
		transform.position = transform.position + (delta * moveSpeed);
		transform.LookAt (player.transform);
	}
	
	public void initializeMonster (GameObject forPlayer)
	{
		health = calculateStartingHealth (forPlayer.transform);
		player = forPlayer;
		setStartingPosition (forPlayer.transform);
		
	}
	
	private int calculateStartingHealth (Transform position)
	{
		return 50;
	}
	
	private void setStartingPosition (Transform playerPosition)
	{
		//set the position
		
		
		float spawnMinDistance = 10;
		float spawnMaxDistance = 30;
		
		float playerX = playerPosition.position.x;
		float playerZ = playerPosition.position.z;
		
		float randomX = spawnMinDistance + (spawnMaxDistance - spawnMinDistance) * Random.value;
		float randomZ = spawnMinDistance + (spawnMaxDistance - spawnMinDistance) * Random.value;
		float positiveOrNegative1 = Random.value > 0.5 ? -1 : 1;
		float positiveOrNegative2 = Random.value > 0.5 ? -1 : 1;
		Vector3 monsterPosition = new Vector3 (playerX + positiveOrNegative1 * randomX, 3, playerZ + positiveOrNegative2 * randomZ);
		gameObject.transform.position = monsterPosition;
		
		//Debug.Log ("Spawning monster at " + monsterPosition);
	}
}
