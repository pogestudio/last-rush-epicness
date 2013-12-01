using UnityEngine;
using System.Collections;

/// <summary>
/// Add this component to a monster you to enable loot dropping for.
/// </summary>
public enum LootTable
{

	CHOOSELOOT = 0,
	RegularLoot = 1,
	MiniBossLoot = 2,
	BossLoot = 3,
}

public class DropsLoot : MonoBehaviour
{
	public LootTable lootTable;
	private bool hasDroppedLoot = false;
	
	//call this method whenever the monster dies. The loottable will decide the chance of dropping.
	public void ShouldDropLoot ()
	{
		if (lootTable == LootTable.CHOOSELOOT) {
			Debug.LogError ("Forgot to chose loot for enemy!!");
		}
		
		if (!hasDroppedLoot) {
			LootHandler.sharedHandler ().createLootForMonsterDeath (lootTable, transform);
			hasDroppedLoot = true;
		}
	}
}