using UnityEngine;
using System.Collections;


/// <summary>
/// Is in charge of deciding all the logic behind the loot.
/// A monster tells if it it has died, with what lootTable.
/// Uses LootFactory to create all the loot, and then sets it up.
/// </summary>
public class LootHandler
{
		private static LootHandler sharedInstance;
	
		public static LootHandler sharedHandler ()
		{
				if (sharedInstance == null) {
						sharedInstance = new LootHandler ();
				}
				return sharedInstance;
		}
	
		/// <summary>
		/// Decides if the loot should be dropped, and creates one and drops if need be. 
		/// </summary>
		/// <param name="lootRarity">Loot table of monster.</param>
		/// <param name="monsterLocation">Monster location.</param>
		public void createLootForMonsterDeath (LootTable lootRarity, Transform monsterLocation)
		{
				if (!shouldWeDropLoot (lootRarity)) {
						return;//bail out if we shouldn't drop any lllooooooooot.
				}
				createLootNoMatterWhat (randomiseWeaponType (), monsterLocation);
		}


		/// <summary>
		/// Used to instantiate a specific weapon at a specific position. The power of the weapon is relatted to its drop location.
		/// Yes, awesome method name. :)
		/// </summary>
		/// <param name="type">type of the weapon to spawn</param>
		/// <param name="location">defines the location where the weapon is spawned, and its stats</param>
		/// <returns></returns>
		public GameObject createLootNoMatterWhat (WeaponTypes type, Transform location)
		{
				GameObject createdWeapon = LootFactory.sharedFactory ().createLoot (type, location);
				fixStatsOfWeapon (createdWeapon, location);
				return createdWeapon;
		}
	
		bool shouldWeDropLoot (LootTable lootRarity)
		{
				float chanceOfDropping = 1F;
				if (lootRarity == LootTable.RegularLoot) {
						chanceOfDropping = 0.05F;
				}
				bool shouldDrop = chanceOfDropping >= Random.value;
				return shouldDrop;
		}
	
		WeaponTypes randomiseWeaponType ()
		{
				int randomValue = Mathf.RoundToInt (Random.Range (0, (float)(WeaponTypes.WeaponTypesMAX - 1)));
				if (randomValue < 0 || randomValue > ((int)WeaponTypes.WeaponTypesMAX) - 1) {
						Debug.LogError ("Wrong value in randomiseWeaponType in loothandler.");
				}
				return (WeaponTypes)randomValue;
		}
	
		void fixStatsOfWeapon (GameObject weapon, Transform monsterLocation)
		{
				AbstractWeapon weaponLogic = weapon.GetComponent<AbstractWeapon> ();
				weaponLogic.weaponDamage = calculateWeaponDamage (weaponLogic.timeBetweenShots, monsterLocation.position);
				Debug.Log ("current weapondamage:: " + weaponLogic.weaponDamage);
		}
	
		int calculateWeaponDamage (float weaponSpeed, Vector3 monsterPosition)
		{
				float dps = dpsForMapPosition (monsterPosition);
				int damagePerShot = (int)(dps * weaponSpeed);
				return damagePerShot;
		}
	
		/// <summary>
		/// Calculate the DPS of the map position. 
		/// </summary>
		/// <returns>The for map position.</returns>
		/// <param name="position">Position.</param>
		float dpsForMapPosition (Vector3 position)
		{
				float centerOfMapAtWhatDistance = 1000F;
				float playerPosition = position.z;
				float playerPositionFromEdge = centerOfMapAtWhatDistance + playerPosition;
				float playerProgressInMap = playerPositionFromEdge / (centerOfMapAtWhatDistance * 2);
				float playerProg = playerProgressInMap * 100; // get real numbers for formula
		
				float maxDPS = 500F;
				float minDPS = 25F;
				float completeProgress = 100F;
		
				float C = Mathf.Pow (10, Mathf.Log10 (maxDPS - minDPS) / completeProgress);
				float CtothePowerOfX = Mathf.Pow (C, playerProg);
				float completeDps = CtothePowerOfX + minDPS;
				//Debug.Log ("completeDps: " + completeDps + " for player progress: " + playerProg);
				//(10^(log(500-25)/100))^99 + 25
				//500 = max dps
				//25 = min dps
				// 100 = max progress
				// 99 = current progress
				return completeDps;
		}
}

