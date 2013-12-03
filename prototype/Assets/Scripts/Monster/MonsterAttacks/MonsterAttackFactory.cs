using UnityEngine;
using System.Collections;

public class MonsterAttackFactory : MonoBehaviour
{

	protected static MonsterAttackFactory instance;
	
	
	public GameObject miniBossHomingProjectile;
	
	
	public static MonsterAttackFactory sharedFactory ()
	{
		if (instance == null) {
			GameObject placeholder = GameObject.Find ("MonsterAttackFactory");
			instance = placeholder.GetComponent<MonsterAttackFactory> ();
		}
		return instance;
	}
	
	public GameObject miniBossProjectile (Transform bossOrigin)
	{
		GameObject newProjectile = Instantiate (miniBossHomingProjectile, bossOrigin.position, bossOrigin.rotation) as GameObject;	
		
		return newProjectile;
	}
	
}

