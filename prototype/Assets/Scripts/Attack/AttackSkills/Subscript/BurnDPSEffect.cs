using UnityEngine;
using System.Collections;

/// <summary>
/// Burn effect. Can be added by any skill that wish to add a burning DOT.
/// Don't forget to set the public variables!
/// </summary>
public class BurnDPSEffect : MonoBehaviour
{
	public float burningDuration;
	public float burningDamage;
	public float burningDamagePerSecond;
	public WeaponTypes weaponToCauseIt;
	
	private float timeToStop;
	private float nextTick;
	
	private Color regularColor;
	private Color burnColor = Color.yellow;
	
	// Use this for initialization
	void Start ()
	{
		//Calculate the damage and how/when to burn
		burningDamagePerSecond = burningDamage / burningDuration;
		timeToStop = Time.time + burningDuration + 0.1F;
		//destroy the script after a while, so that the color disappears
		Destroy (this, burningDuration + 0.5F);
		
		//save the color so we can set it back at the end. set a burnColor for the duration.
		regularColor = gameObject.transform.renderer.material.color;
		gameObject.transform.renderer.material.color = burnColor;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("Time: " + Time.time + "    Time to stop");
		if (Time.time <= timeToStop && Time.time >= nextTick) {
			//Debug.Log ("TICKING DAMAGE " + Time.time);
			nextTick = Time.time + 1F;
			Projectile.wantToDamage (gameObject, (int)Mathf.Ceil (burningDamagePerSecond), weaponToCauseIt);
		} 
	}
	
	
	void OnDestroy ()
	{
		gameObject.transform.renderer.material.color = regularColor;
	}
}

