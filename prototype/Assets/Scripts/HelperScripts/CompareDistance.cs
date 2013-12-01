using UnityEngine;
using System.Collections;

public class CompareDistance: IComparer
{	
	private Vector3 centerPoint;
	public CompareDistance (Vector3 fromThisPoint)
	{
		this.centerPoint = fromThisPoint;
	}
	public int  Compare (object first, object second)
	{
		float distance1 = Vector3.Distance (((GameObject)first).transform.position, centerPoint);
		float distance2 = Vector3.Distance (((GameObject)second).transform.position, centerPoint);
		int result;
		if (distance1 < distance2) {
			result = -1;
		} else {
			result = 1;
		}
		return result;
	}
}