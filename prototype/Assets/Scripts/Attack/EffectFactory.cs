using UnityEngine;
using System.Collections;

public class EffectFactory : MonoBehaviour
{
	
	protected static EffectFactory instance;
	
	//THE AMMO
	public GameObject bullet;
	
	
	public static EffectFactory sharedFactory ()
	{
		return instance;
	}
	// Use this for initialization
	void Start ()
	{
		instance = this;
	}
	
}