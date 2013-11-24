using UnityEngine;
using System.Collections;

public class MonsterFactory : MonoBehaviour
{
	protected static MonsterFactory instance; // Needed
	
	public GameObject regularMonster;
	private GameObject miniBoss;
	private GameObject majorBoss;
	
	void Start ()
	{
		instance = this;
		//regularMonster = GameObject.Find ("RegularMonster");
		if (!regularMonster)
			Debug.LogError ("don't have a regular monster. Fail Fail Fail");
	}
	
	public static void SpawnMonster (GameObject forPlayer)
	{
		GameObject monster = Object.Instantiate (instance.regularMonster, Vector3.zero, Quaternion.identity) as GameObject;
		MonsterLogic monstersLogic = monster.GetComponent ("MonsterLogic") as MonsterLogic;
		monstersLogic.initializeMonster (forPlayer);
	}
	
}
