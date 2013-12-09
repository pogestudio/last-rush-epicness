using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

    public float pulseSpeed = 1;
    public float pulseWidth = 0.5f;

    private Vector3 baseScale;
    private float time;

	// Use this for initialization
	void Start () {
        baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        transform.localScale = baseScale * (Mathf.Sin(time * pulseSpeed)*pulseWidth + 1f);
	}
}
