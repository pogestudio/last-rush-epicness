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
	
	public GameObject deliverSmallExplosion (Transform effectOrigin)
	{
		return returnObject (smallExplosion, effectOrigin);
	}
	
	public GameObject deliverSphericNova (Transform effectOrigin)
	{
		return returnObject (sphericNova, effectOrigin);
	}
	
	public GameObject deliverSnowingFire (Transform effectOrigin)
	{
		return returnObject (snowingFire, effectOrigin);
	}
	
	private GameObject returnObject (GameObject effect, Transform effectOrigin)
	{
		return Instantiate (effect, effectOrigin.position, effectOrigin.rotation) as GameObject;
	}
}