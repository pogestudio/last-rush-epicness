using UnityEngine;
using System.Collections;

/// <summary>
/// Slowing effect. Add to objects that should be slowed. 
/// SOMEWHAT COMPLICATED TO ADD, see frost nova example. 
/// </summary>

public class SlowingEffect : MonoBehaviour
{
	public float slowingDuration;
	public float slowingPercentage;
	
	private float timeToStop;
	private Color regularColor;
	private Color slowColor = Color.blue;
	private MonsterLogic monsterLogic;
	
	// Use this for initialization
	void Start ()
	{
		monsterLogic = gameObject.GetComponent<MonsterLogic> ();
		timeToStop = Time.time + slowingDuration;
		//save the color so we can set it back at the end. set a burnColor for the duration.
		regularColor = gameObject.transform.renderer.material.color;
		startSlowing ();
	}
	
	void Update ()
	{
		if (Time.time >= timeToStop) {
			Component.Destroy (this);
		}
	}
	
	void OnDestroy ()
	{
		removeSlowing ();
		gameObject.transform.renderer.material.color = regularColor;
	}
	
	void startSlowing ()
	{
		//Debug.Log ("Started slowing");
		gameObject.transform.renderer.material.color = slowColor;
		monsterLogic.movingSpeed = monsterLogic.movingSpeed * (1 - slowingPercentage);
		
	}
	
	void removeSlowing ()
	{
		//Debug.Log ("Removed slowing");
		if (gameObject.activeSelf) {
			monsterLogic.movingSpeed = monsterLogic.movingSpeed / (1 - slowingPercentage);
		}
	}
	
	public void updateTimer ()
	{
		timeToStop = Time.time + slowingDuration;
		
	}
}

