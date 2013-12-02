using UnityEngine;
using System.Collections;

public class rotateTest : MonoBehaviour {

    [Range(0,100)]
    public float speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);
	}
}
