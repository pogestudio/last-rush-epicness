using UnityEngine;
using System.Collections;

public class LevelUpEffect : MonoBehaviour {

	private float duration = 2.0f;
	private float param = 0f;

	private Quaternion lastRotation;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, duration);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = lastRotation;
		transform.Rotate(transform.up, 1.0f);

		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sprite in sprites) {
			Color c = sprite.color;
			c.a = 0.5f * Mathf.Sin (param);
			sprite.color = c;
		}

		param = param + Time.deltaTime * Mathf.PI / duration;
		lastRotation = transform.rotation;
	}
}
