using UnityEngine;
using System.Collections;

/*

This is monster logic, and decides all monster related behaviour.
health - the health of the monster

movingSpeed - decides how fast the monster is moving


GUIDING SHOULD BE REFACTORED to a seperate component. 

*/

public class MonsterLogic : AbstractEnemy
{

	private float despawnDistance = 80F;
	
	// Update is called once per frame
	void Update ()
	{		
		if (target) {
			walk ();
		} else {
			target = PlayerFinder.sharedHelper ().getClosestPlayer (transform.position, searchRadius);
		}
		despawnIfTooFar ();
		
	}
	
	void despawnIfTooFar ()
	{
		float distance = Vector3.Distance (target.transform.position, transform.position);
		if (distance > despawnDistance) {
			Destroy (gameObject);
		}
	}
}
