using UnityEngine;
using System.Collections;

public class PlayerFinder
{
	
	static PlayerFinder sharedInstance;
	
	public static PlayerFinder sharedHelper ()
	{
		if (sharedInstance == null) {
			sharedInstance = new PlayerFinder ();
		}
		return sharedInstance;
	}
	
	public GameObject getClosestPlayer (Vector3 center, float searchRadius)
	{
		ArrayList sortedPlayersAroundLocation = getSortedListOfPlayers (center, searchRadius);
		return firstOrNull (sortedPlayersAroundLocation);
	}
	
	private ArrayList getSortedListOfPlayers (Vector3 center, float searchRadius)
	{
		ArrayList playersAroundLocation = playersWithinArea (center, searchRadius);
//		foreach (GameObject monster in playersAroundLocation) {
//			Debug.Log ("Monster position: " + monster.transform.position);
//		}
		CompareDistance comparer = new CompareDistance (center);
		playersAroundLocation.Sort (comparer);
		
		//Debug.Log ("Player SORTED!");
		
//		foreach (GameObject monster in playersAroundLocation) {
//			Debug.Log ("player position: " + monster.transform.position);
//		}
		
		return playersAroundLocation;
	}
	
	private GameObject firstOrNull (ArrayList list)
	{
		if (list.Count > 0) {
			return (GameObject)list [0];
		} else {
			return null;
		}
	}
	
	
	public ArrayList playersWithinArea (Vector3 center, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);
		int i = 0;
		ArrayList players = new ArrayList ();
		
		while (i < hitColliders.Length) {
			if (targetIsPlayer (hitColliders [i].gameObject)) {
				players.Add (hitColliders [i].gameObject);
			}
			i++;
		}
		
		return players;
	}
	
	public bool targetIsPlayer (GameObject target)
	{
		return (target.CompareTag("Player"));
	}
	
}

