using UnityEngine;
using System.Collections;
/// <summary>
/// Sets up a lightning strike to a target, and jumps from target to target!
/// TODO: still belongs to the original target, so it despawns if killed halfway through.
/// </summary>
public class LightningStrike : MonoBehaviour
{

	public static LineRenderer staticRenderer;
	
	public int amountOfJumps = 10; //default amount.
	public int shotDamage;
	public WeaponTypes weaponToCauseIt;
	
	
	public float damageMultiplier = 0.3F;
	private float damagePerJump;
	private float timeBetweenJumps = 0.05F;
	private float nextJump;
	
	private float searchRadius = 10F;
	public GameObject currentMonster;
	private GameObject nextMonster;
	
	public ArrayList ignoreList = new ArrayList (); //we will add all previously visited targets here, so we don't get a loop

	// Use this for initialization
	void Start ()
	{
		damagePerJump = damageMultiplier * shotDamage;
		ignoreList.Add (currentMonster);
		setNewTarget ();
		nextJump = Time.time + timeBetweenJumps;
		amountOfJumps--;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (nextMonster == null || amountOfJumps <= 0 || currentMonster == null) {
			//bail out if there are no monsters nearby. 
			Destroy (this);
		}
		
		
		if (Time.time > nextJump) {
			//Debug.Log ("Jumping!");
			SkillEffect.wantToDamage (nextMonster, (int)damagePerJump, weaponToCauseIt);
			showOneLightningBolt ();
			addNextLightningToNextMonster ();
		}
	}
	
	void showOneLightningBolt ()
	{
		EffectFactory.sharedFactory ().createLightningBetween (currentMonster.transform.position, nextMonster.transform.position);			
	}
	
	void setNewTarget ()
	{
		nextMonster = MonsterFinder.sharedHelper ().getClosestMonsterExcept (ignoreList, gameObject.transform.position, searchRadius);
	}

	void addNextLightningToNextMonster ()
	{
		LightningStrike lStrike = nextMonster.AddComponent<LightningStrike> ();
		lStrike.shotDamage = this.shotDamage;
		lStrike.damageMultiplier = this.damageMultiplier;
		lStrike.currentMonster = this.currentMonster;
		lStrike.weaponToCauseIt = this.weaponToCauseIt;
		lStrike.amountOfJumps = this.amountOfJumps - 1;
		lStrike.nextJump = this.nextJump + this.timeBetweenJumps;
		lStrike.ignoreList = this.ignoreList;
	}
}

