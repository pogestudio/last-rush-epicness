using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {

    private Light[] subLights;

	// Use this for initialization
	void Awake () {
        subLights = transform.GetComponentsInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void toggleOnOff(bool on)
    {
        foreach(Light light in subLights)
        {
            light.enabled = on;
        }
    }
}
