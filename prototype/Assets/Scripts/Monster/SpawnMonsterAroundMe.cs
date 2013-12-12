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
		
				if (!networkView.isMine) {
						this.enabled = false;
						
				}
				Debug.Log ("Network view ismine:: " + networkView.isMine);


				if (spawnRate == 0F)
						Debug.LogError ("spawnRate should be set in scene. is not!");	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (timeToIgnore > Time.time) {
						// do nothing
				
				} else if (reachMaxSpawnRateAfterThisTime > Time.time && Time.time > nextSpawn) { //spawn slowly!
						MonsterFactory.SpawnMonster (gameObject);
						float additionalSpawnTime = Time.time * (-0.125F) + 10;
						nextSpawn = Time.time + spawnRate + additionalSpawnTime;
						Debug.Log ("Deprecated spawn. Next spawn rate " + nextSpawn);
				} else if (Time.time > nextSpawn) {	//regular spawn!
						Debug.Log ("Regular spawn");
						MonsterFactory.SpawnMonster (gameObject);
						nextSpawn = calculateNextSpawnRate ();
				}
		}
		
		float calculateNextSpawnRate ()
		{
				float centerOfMapAtWhatDistance = 1000F;
				float playerPosition = transform.position.z;
				float playerPositionFromEdge = centerOfMapAtWhatDistance + playerPosition;
				float playerProgressInMap = playerPositionFromEdge / (centerOfMapAtWhatDistance * 2);
				
				float spawnRateIncreaseAtTop = 4;
				float spawnRateIncreaseAtPlayer = Mathf.Max (1, playerProgressInMap * spawnRateIncreaseAtTop);
				
				return Time.time + spawnRate / spawnRateIncreaseAtPlayer;
		}
}
