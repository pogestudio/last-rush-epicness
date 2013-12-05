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
	
	private LineRenderer staticLightningLineRenderer;
	
	
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
	
	public void createLightningBetween (Vector3 originPos, Vector3 destPos)
	{	
		Debug.Log ("create lightning called");
		if (staticLightningLineRenderer == null) {
			GameObject placeHolder = GameObject.Find ("LightningLineRenderer");
			staticLightningLineRenderer = placeHolder.GetComponent<LineRenderer> ();
		}
		LineRenderer lightningLineRenderer = Instantiate (staticLightningLineRenderer, originPos, staticLightningLineRenderer.transform.rotation) as LineRenderer;
		
		float arcLength = 1F;
		float arcVariation = 0.5F;
		float inaccuracy = 0.5F;
		
		Vector3 lastPoint = originPos;
		int i = 1;
		lightningLineRenderer.SetVertexCount (1);
		lightningLineRenderer.SetPosition (0, originPos);//make the origin of the LR the same as the transform
		
		while (Vector3.Distance(destPos, lastPoint) >.5) {//was the last arc not touching the target? 
			lightningLineRenderer.SetVertexCount (i + 1);//then we need a new vertex in our line renderer
			Vector3 fwd = destPos - lastPoint;//gives the direction to our target from the end of the last arc
			fwd.Normalize ();//makes the direction to scale
			fwd = Randomize (fwd, inaccuracy);//we don't want a straight line to the target though
			fwd *= Random.Range (arcLength * arcVariation, arcLength);//nature is never too uniform
			fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
			lightningLineRenderer.SetPosition (i, fwd);//this tells the line renderer where to draw to
			i++;
			lastPoint = fwd;//so we know where we are starting from for the next arc
		}
		
		//DONE! destroy lineRenderer
		Destroy (lightningLineRenderer, 0.2F);
	}
	
	Vector3 Randomize (Vector3 v3, float inaccuracy2)
	{ 
		v3 += new Vector3 (Random.Range (-1.0F, 1.0F), Random.Range (-1.0F, 1.0F), Random.Range (-1.0F, 1.0F)) * inaccuracy2; 
		v3.Normalize (); 
		return v3; 
	}
}