using UnityEngine;
using System.Collections;

public class PowerUpItem : MonoBehaviour
{

		private float timeForPowerUpToLast = 10F;
		public float timeToLive = 120F;
		
		void OnStart ()
		{
				Destroy (gameObject, timeToLive);
		}
		
		void OnCollisionEnter (Collision collidingObject)
		{

            if (collidingObject.gameObject.tag != "Player") //avoids player replicas
						return;
		
				ImprovedSkillChance powerUp = collidingObject.gameObject.AddComponent<ImprovedSkillChance> ();
				powerUp.powerupItem = this.gameObject;
				powerUp.powerupPlayer = collidingObject.gameObject;
				Destroy (powerUp, timeForPowerUpToLast);
				
				Destroy (gameObject);
		}
}

