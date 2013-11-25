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
	
	void Start ()
	{
		instance = this;
		
		/*enable one of these for every monster we have implemented so
		 we can see if something breaks */
		if (!regularMonster)
			Debug.LogError ("don't have a regular monster. Fail Fail Fail");
	}
	
	//////////////////////////// BEGIN REGULAR MONSTER
	//put everything regarding regular monster in here
	
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
		monster.player = forPlayer;
		
	}
	
	private int monsterStartingHealth (Transform position)
	{
		return 50;
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
		Vector3 monsterPosition = new Vector3 (playerX + positiveOrNegative1 * randomX, 3, playerZ + positiveOrNegative2 * randomZ);
		//Debug.Log ("Spawning monster at " + monsterPosition);
		return monsterPosition;
		
	}
	//////////////////////////// END REGULAR MONSTER
	
}
