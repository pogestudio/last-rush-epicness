using UnityEngine;
using System.Collections;

public class MonsterFinder
{

	static MonsterFinder sharedInstance;
	
	public static MonsterFinder sharedHelper ()
	{
		if (sharedInstance == null) {
			sharedInstance = new MonsterFinder ();
		}
		return sharedInstance;
	}
	
	public GameObject getClosestMonster (Vector3 center, float searchRadius)
	{
		ArrayList sortedMonstersAroundLocation = getSortedListOfNearbyMonsters (center, searchRadius);
		return firstOrNull (sortedMonstersAroundLocation);
	}
	
	private ArrayList getSortedListOfNearbyMonsters (Vector3 center, float searchRadius)
	{
		ArrayList monstersAroundLocation = monstersWithinArea (center, searchRadius);
		/*foreach (GameObject monster in monstersAroundMe) {
			Debug.Log ("Monster position: " + monster.transform.position);
		}*/
		CompareDistance comparer = new CompareDistance (center);
		monstersAroundLocation.Sort (comparer);
		
		//Debug.Log ("Monster SORTED!");
		
		/*foreach (GameObject monster in monstersAroundMe) {
			Debug.Log ("Monster position: " + monster.transform.position);
		}*/
		
		return monstersAroundLocation;
	}
	
	public GameObject getClosestMonsterExcept (GameObject thisMonster, Vector3 center, float searchRadius)
	{
		ArrayList monstersAroundLocation = getSortedListOfNearbyMonsters (center, searchRadius);
		monstersAroundLocation.Remove (thisMonster);
		return firstOrNull (monstersAroundLocation);
	}
	
	public GameObject getClosestMonsterExcept (ArrayList listOfExcepts, Vector3 center, float searchRadius)
	{
		ArrayList monstersAroundLocation = getSortedListOfNearbyMonsters (center, searchRadius);
		foreach (GameObject exception in listOfExcepts) {
			monstersAroundLocation.Remove (exception);
		}
		return firstOrNull (monstersAroundLocation);
	}
	
	private GameObject firstOrNull (ArrayList list)
	{
		if (list.Count > 0) {
			return (GameObject)list [0];
		} else {
			return null;
		}
	}
	
	
	public ArrayList monstersWithinArea (Vector3 center, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);
		int i = 0;
		ArrayList monsters = new ArrayList ();
		
		while (i < hitColliders.Length) {
			if (targetIsEnemy (hitColliders [i].gameObject)) {
				monsters.Add (hitColliders [i].gameObject);
			}
			i++;
		}
		
		return monsters;
	}
	
	public bool targetIsEnemy (GameObject target)
	{
		return (target != null && (target.tag == "RegularMonster" || target.tag == "MiniBoss"));
	}
	
}

