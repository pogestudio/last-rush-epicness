using UnityEngine;
using System.Collections;

public class SpawnMiniBossWhenPlayerIsNear : MonoBehaviour
{
	public GameObject miniBoss;
	private float nextScan;
	private GameObject playerTarget;
	public float spawnArea = 50F;
	private bool hasSpawned;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if (Time.time > nextScan) {
			scanForPlayer ();
			nextScan = Time.time + 1.0F;
		}
		
		if (playerTarget != null) {
			createMiniBoss ();
			Destroy (gameObject);
		}
	}
	
	void scanForPlayer ()
	{
        //Debug.Log ("scanning for player, current pos is: " + transform.position);
		playerTarget = PlayerFinder.sharedHelper ().getClosestPlayer (transform.position, spawnArea);
	}
	
	void createMiniBoss ()
	{
        Debug.Log("spawning boss");
		if (!hasSpawned) {
			GameObject newBoss = Instantiate (miniBoss, transform.position, transform.rotation) as GameObject;
			hasSpawned = true;
		}
		Destroy (gameObject);
	}
}
