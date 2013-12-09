using UnityEngine;
using System.Collections;

public class WeaponGlow : MonoBehaviour {

    public float pulseSize = 0.5f;
    public float pulseSpeed = 5f;

    private Vector3 baseScale;

	// Use this for initialization
	void Start () {
        baseScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {
        transform.localScale = baseScale * ((Mathf.Sin(Time.time * pulseSpeed) +2.1f)* pulseSize);
	}
}
