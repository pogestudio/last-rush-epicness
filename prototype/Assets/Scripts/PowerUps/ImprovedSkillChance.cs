using UnityEngine;
using System.Collections;

public class ImprovedSkillChance : MonoBehaviour {
	private float chanceToImprove = 100;

	public GameObject powerupItem;
	public GameObject powerupPlayer;

	private GameObject particleEffect;

	// Use this for initialization
	void Start ()
	{
		//add animation
		particleEffect = powerupItem.transform.GetComponentInChildren<ParticleSystem>().gameObject;
		if (particleEffect) {
			particleEffect.transform.parent = powerupPlayer.transform;
			particleEffect.transform.localPosition = Vector3.zero;
		}



	}

	public float getImprovedChance ()
	{
		return chanceToImprove;
	}

	void OnDestroy ()
	{
		//remove animation
		Destroy (particleEffect);
	}

}

