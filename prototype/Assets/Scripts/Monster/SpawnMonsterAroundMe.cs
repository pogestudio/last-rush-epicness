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
		public float reachMaxSpawnRateAfterThisTime = 80;
		public float timeToIgnore = 20;
		
		

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
				if (timeToIgnore > Time.time)
						return;
				if (reachMaxSpawnRateAfterThisTime > Time.time && Time.time > nextSpawn) { //spawn slowly!
						MonsterFactory.SpawnMonster (gameObject);
						nextSpawn = Time.time + (spawnRate) * (reachMaxSpawnRateAfterThisTime - Time.time) / reachMaxSpawnRateAfterThisTime;
				} else if (Time.time > nextSpawn) {	//regular spawn!
						MonsterFactory.SpawnMonster (gameObject);
						nextSpawn = Time.time + spawnRate;
				}
		}
}
