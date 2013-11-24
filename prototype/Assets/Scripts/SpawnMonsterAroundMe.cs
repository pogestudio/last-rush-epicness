using UnityEngine;
using System.Collections;
/// <summary>
/// Spawn monster around me. Add this component to have it 
/// call Monster Factory at regular intervals.
/// </summary>
public class SpawnMonsterAroundMe : MonoBehaviour
{

	public float spawnRate = 1.0F;
	private float nextSpawn = 0.0F;

	// Use this for initialization
	void Start ()
	{
		
		
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
