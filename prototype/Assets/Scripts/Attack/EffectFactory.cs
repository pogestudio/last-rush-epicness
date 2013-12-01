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
	
	
	public static EffectFactory sharedFactory ()
	{
		return instance;
	}
	// Use this for initialization
	void Start ()
	{
		instance = this;
	}
	
	public GameObject deliverSmallExplosion (Transform effectOrigin)
	{
		return returnObject (smallExplosion, effectOrigin);
	}
	
	public GameObject deliverSphericNova (Transform effectOrigin)
	{
		return returnObject (sphericNova, effectOrigin);
	}
	
	private GameObject returnObject (GameObject effect, Transform effectOrigin)
	{
		return Instantiate (effect, effectOrigin.position, effectOrigin.rotation) as GameObject;
	}
}