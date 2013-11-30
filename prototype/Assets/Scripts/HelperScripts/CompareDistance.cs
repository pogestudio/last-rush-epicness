using UnityEngine;
using System.Collections;

public class CompareDistance: IComparer
{	
	private GameObject originalObject;
	public CompareDistance (GameObject originalObject)
	{
		this.originalObject = originalObject;
	}
	public int  Compare (object first, object second)
	{
		GameObject firstGO = ((GameObject)first);
		GameObject secondGO = (GameObject)second;
		float distance1 = Vector3.Distance (((GameObject)first).transform.position, originalObject.transform.position);
		float distance2 = Vector3.Distance (((GameObject)second).transform.position, originalObject.transform.position);
		int result;
		if (distance1 < distance2) {
			result = -1;
		} else {
			result = 1;
		}
		return result;
	}
}