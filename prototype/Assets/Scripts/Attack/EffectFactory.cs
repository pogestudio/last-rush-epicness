using UnityEngine;
using System.Collections;

public class EffectFactory : MonoBehaviour
{
	
	protected static EffectFactory instance;
	
	//THE AMMO
	public GameObject smallExplosion;
	
	
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
		GameObject explosion = Instantiate (smallExplosion, effectOrigin.position, effectOrigin.rotation) as GameObject;
		return explosion;
	}
}