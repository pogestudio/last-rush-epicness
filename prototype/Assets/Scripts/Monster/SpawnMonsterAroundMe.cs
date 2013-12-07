using UnityEngine;
using System.Collections;
/// <summary>
/// Spawn monster around me. Add this component to have it 
/// call Monster Factory at regular intervals.
/// </summary>
public class SpawnMonsterAroundMe : MonoBehaviour
{

	public float spawnRate = 0F; //THIS SHOULD BE SET IN SCENE
	private float nextSpawn = 0.0F;

	// Use this for initialization
	void Start ()
	{
        if (!networkView.isMine)
            this.enabled = false;

		if (spawnRate == 0F)
			Debug.LogError ("spawnRate should be set in scene. is not!");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > nextSpawn) {
			MonsterFactory.SpawnMonster (gameObject);
			nextSpawn = Time.time + spawnRate;
		}
	}
}
