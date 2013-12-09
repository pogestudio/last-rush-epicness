using UnityEngine;
using System.Collections;

public class MonsterAttackFactory : MonoBehaviour
{

		protected static MonsterAttackFactory instance;
	
	
		public GameObject miniBossHomingProjectile;
		public GameObject miniBossStraightProjectile;
	
	
		public static MonsterAttackFactory sharedFactory ()
		{
				if (instance == null) {
						GameObject placeholder = GameObject.Find ("MonsterAttackFactory");
						instance = placeholder.GetComponent<MonsterAttackFactory> ();
				}
				return instance;
		}
	
		public GameObject createMiniBossHomingProjectile (Transform bossOrigin)
		{
				GameObject newProjectile = Instantiate (miniBossHomingProjectile, bossOrigin.position, bossOrigin.rotation) as GameObject;	
		
				return newProjectile;
		}
	
		public GameObject createMiniBossStraightProjectile (Transform bossOrigin)
		{
				GameObject newProjectile = Instantiate (miniBossStraightProjectile, bossOrigin.position, bossOrigin.rotation) as GameObject;	
		
				return newProjectile;
		}
	
	
	
}

