using UnityEngine;
using System.Collections;
/// <summary>
/// Monster factory creates monsters around a player.
/// It clones them and sets them up for game play mode. 
/// </summary>
public class MonsterFactory : MonoBehaviour
{
	protected static MonsterFactory instance; // Needed
	
	//connect these to the prefabs we want to use
	public GameObject regularMonster;
	/*private GameObject miniBoss;
	private GameObject majorBoss;*/
	
	private float mapHeight;
	
	void Start ()
	{
		instance = this;
		
		/*enable one of these for every monster we have implemented so
		 we can see if something breaks */
		if (!regularMonster)
			Debug.LogError ("don't have a regular monster. Fail Fail Fail");
			
		GameObject terrainObject = GameObject.Find ("Terrain");
		Terrain terrain = terrainObject.GetComponent<Terrain> ();
		mapHeight = terrain.terrainData.size.z;
	}
	
	//////////////////////////// BEGIN REGULAR MONSTER
	//put everything regarding regular monster in here
	
	/// <summary>
	/// Spawns a mini boss. 
	/// </summary>
	/// <param name="forPlayer">Player to spawn around</param>
	public static void SpawnMiniBoss (GameObject forPlayer, Vector3 atPosition)
	{
		GameObject monster = Object.Instantiate (instance.regularMonster, Vector3.zero, Quaternion.identity) as GameObject;
		MonsterLogic monstersLogic = monster.GetComponent ("MonsterLogic") as MonsterLogic;
		instance.initializeMonster (monstersLogic, forPlayer);
		monster.transform.position = instance.monsterStartingPosition (forPlayer.transform);
	}
	
	/// <summary>
	/// Spawns a regular monster. 
	/// </summary>
	/// <param name="forPlayer">Player to spawn around</param>
	public static void SpawnMonster (GameObject forPlayer)
	{
		GameObject monster = Object.Instantiate (instance.regularMonster, Vector3.zero, Quaternion.identity) as GameObject;
		MonsterLogic monstersLogic = monster.GetComponent ("MonsterLogic") as MonsterLogic;
		instance.initializeMonster (monstersLogic, forPlayer);
		monster.transform.position = instance.monsterStartingPosition (forPlayer.transform);
	}
	
	public void initializeMonster (MonsterLogic monster, GameObject forPlayer)
	{
		monster.health = monsterStartingHealth (forPlayer.transform);
		monster.target = forPlayer;
		
	}
	
	private int monsterStartingHealth (Transform position)
	{
		float centerOfMapAtWhatDistance = 1000F;
		float playerPosition = position.position.z;
		float playerPositionFromEdge = centerOfMapAtWhatDistance + playerPosition;
		float playerProgressInMap = playerPositionFromEdge / mapHeight;
		//Debug.Log("player progress : " + playerProgressInMap);
		
		float monsterHealth = 300 * Mathf.Pow (40, playerProgressInMap) - 300;
		monsterHealth = Mathf.Max (4, monsterHealth);
		//Debug.Log("SPAWNING MOnster with health " + monsterHealth);
		return (int)monsterHealth;
	}
	
	public Vector3 monsterStartingPosition (Transform playerTransform)
	{			
		float spawnMinDistance = 10;
		float spawnMaxDistance = 30;
		
		float playerX = playerTransform.position.x;
		float playerZ = playerTransform.position.z;
		
		float randomX = spawnMinDistance + (spawnMaxDistance - spawnMinDistance) * Random.value;
		float randomZ = spawnMinDistance + (spawnMaxDistance - spawnMinDistance) * Random.value;
		float positiveOrNegative1 = Random.value > 0.5 ? -1 : 1;
		float positiveOrNegative2 = Random.value > 0.5 ? -1 : 1;
		Vector3 monsterPosition = new Vector3 (playerX + positiveOrNegative1 * randomX, playerTransform.position.y, playerZ + positiveOrNegative2 * randomZ);
		//Debug.Log("Spawning monster at " + monsterPosition);
		return monsterPosition;
		
	}
	//////////////////////////// END REGULAR MONSTER
	
}
