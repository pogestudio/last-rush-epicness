using UnityEngine;
using System.Collections;

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
	private LineRenderer lightningLineRenderer;
	
	private float arcLength = 1F;
	private float arcVariation = 0.5F;
	private float inaccuracy = 0.5F;
	
	private ArrayList ignoreList = new ArrayList (); //we will add all previously visited targets here, so we don't get a loop

	// Use this for initialization
	void Start ()
	{
		if (staticRenderer == null) {
			GameObject placeHolder = GameObject.Find ("LightningLineRenderer");
			staticRenderer = placeHolder.GetComponent<LineRenderer> ();
		}
		lightningLineRenderer = staticRenderer;
		if (!lightningLineRenderer)
			Debug.LogError ("No LightningLineRenderer!");
			
		
		damagePerJump = damageMultiplier * shotDamage;
		
		ignoreList.Add (currentMonster);
		setNewTarget ();
		nextJump = Time.time + timeBetweenJumps;
		amountOfJumps--;
		lightningLineRenderer.SetVertexCount (1);
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (amountOfJumps <= 0 || !currentMonster.activeSelf) {
			Destroy (this);
			return;
		}
		
		if (Time.time > nextJump || nextMonster == null || !nextMonster.activeSelf) {
			//Debug.Log ("Jumping!");
			jumpTargets ();
			GameObject monsterToReceiveDamage = nextMonster != null ? nextMonster : currentMonster;
			SkillEffect.wantToDamage (monsterToReceiveDamage, (int)damagePerJump, weaponToCauseIt);
			nextJump += timeBetweenJumps;
			amountOfJumps--;
		} else {
			showOneLightningBolt ();
		}
		
	}
	
	void showOneLightningBolt ()
	{
		Vector3 lastPoint = currentMonster.transform.position;
		int i = 1;
		lightningLineRenderer.SetVertexCount (1);
		lightningLineRenderer.SetPosition (0, currentMonster.transform.position);//make the origin of the LR the same as the transform
		
		while (Vector3.Distance(nextMonster.transform.position, lastPoint) >.5) {//was the last arc not touching the target? 
			lightningLineRenderer.SetVertexCount (i + 1);//then we need a new vertex in our line renderer
			Vector3 fwd = nextMonster.transform.position - lastPoint;//gives the direction to our target from the end of the last arc
			fwd.Normalize ();//makes the direction to scale
			fwd = Randomize (fwd, inaccuracy);//we don't want a straight line to the target though
			fwd *= Random.Range (arcLength * arcVariation, arcLength);//nature is never too uniform
			fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
			lightningLineRenderer.SetPosition (i, fwd);//this tells the line renderer where to draw to
			i++;
			lastPoint = fwd;//so we know where we are starting from for the next arc
		}
	}
	
	Vector3 Randomize (Vector3 v3, float inaccuracy2)
	{ 
		v3 += new Vector3 (Random.Range (-1.0F, 1.0F), Random.Range (-1.0F, 1.0F), Random.Range (-1.0F, 1.0F)) * inaccuracy2; 
		v3.Normalize (); 
		return v3; 
	}
	
	void setNewTarget ()
	{
		nextMonster = MonsterFinder.sharedHelper ().getClosestMonsterExcept (ignoreList, gameObject.transform.position, searchRadius);
	}
	
	void jumpTargets ()
	{
		if (nextMonster != null) {
			ignoreList.Add (nextMonster);
			currentMonster = nextMonster;
		}
		setNewTarget ();
	}
	
	void OnDestroy ()
	{
		lightningLineRenderer.SetVertexCount (0);
	}
}

