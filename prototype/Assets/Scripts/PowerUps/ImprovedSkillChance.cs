using UnityEngine;
using System.Collections;

public class ImprovedSkillChance : MonoBehaviour
{
		private float chanceToImprove = 100;
		// Use this for initialization
		void Start ()
		{
				//add animation
		}
		
		public float getImprovedChance ()
		{
				return chanceToImprove;
		}
		
		void OnDestroy ()
		{
				//remove animation
		}
	
}

