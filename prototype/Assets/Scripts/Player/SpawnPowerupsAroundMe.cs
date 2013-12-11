using UnityEngine;
using System.Collections;

public class SpawnPowerupsAroundMe : MonoBehaviour
{

		public GameObject skillChancePowerup;
		public float powerUpsPerMinute = 0.5F;
		private float nextSecond;

	
		// Update is called once per frame
		void Update ()
		{
				if (Time.time > nextSecond) {
						nextSecond = Time.time + 1F;
						if (shouldSpawnPowerup ()) {
								spawnPowerUpAroundPlayer ();
						}
				}
		}
		
		/// <summary>
		/// should be called once every second.
		/// </summary>
		/// <returns><c>true</c>, if  powerup should spawn, <c>false</c> otherwise.</returns>
		private bool shouldSpawnPowerup ()
		{
				float powerUpsPerSecond = powerUpsPerMinute / 60F;
				return (Random.value < powerUpsPerSecond);
		}
		
		private void spawnPowerUpAroundPlayer ()
		{
				float randomVectorSize = 10F;
				Vector3 randomVector1 = new Vector3 (Random.Range (-1, 1), 0.2F, Random.Range (-1, 1)) * randomVectorSize;
				Instantiate (skillChancePowerup, gameObject.transform.position + randomVector1, gameObject.transform.rotation);
		}
}
