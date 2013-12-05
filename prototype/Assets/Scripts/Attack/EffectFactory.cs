using UnityEngine;
using System.Collections;

/// <summary>
/// EffectFactory will deliver visual stuff to objects that want them.
/// </summary>
public class EffectFactory : MonoBehaviour
{
	
	protected static EffectFactory instance;
	
	//THE AMMO
	public GameObject smallExplosion;
	public GameObject sphericNova;
	public GameObject snowingFire;
	
	
	public static EffectFactory sharedFactory ()
	{
		if (instance == null) {
			GameObject placeholder = GameObject.Find ("EffectFactory");
			instance = placeholder.GetComponent<EffectFactory> ();
		}
		return instance;
	}
	
	public void deliverSmallExplosion (Transform effectOrigin)
	{
		instantiateObject (smallExplosion, effectOrigin);
	}
	
	public void deliverSphericNova (Transform effectOrigin)
	{
		instantiateObject (sphericNova, effectOrigin);
	}
	
	public void createMiniBossChargingEffect (GameObject miniBoss, float timeToDestroy)
	{
		GameObject chargingEffect = instantiateObject (snowingFire, miniBoss.transform);
		chargingEffect.transform.parent = miniBoss.transform;
		chargingEffect.transform.localPosition = new Vector3 (0, -0.2F, 0);
		chargingEffect.transform.localScale = new Vector3 (1, 1, 1);
		chargingEffect.transform.Rotate (-90F, 0, 0);
		chargingEffect.SetActive (true);
		Destroy (chargingEffect, timeToDestroy);
	}
	
	private GameObject instantiateObject (GameObject effect, Transform effectOrigin)
	{
		return Instantiate (effect, effectOrigin.position, effectOrigin.rotation) as GameObject;
	}
}