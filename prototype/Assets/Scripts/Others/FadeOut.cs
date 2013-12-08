using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

	private Shader[] originalShaders;

	private float targetAlpha = 1.0f;
	private float currentAlpha = 1.0f;

	//private bool fadeStarted = false;

	private float changeSpeed = 0.05f;
	//private float changeFactor = 0.5f;

	// Use this for initialization
	void Start () {
		originalShaders = new Shader[renderer.materials.Length];

		for (int i = 0; i < renderer.materials.Length; i++) {
			originalShaders[i] = renderer.materials[i].shader;
		}
	}

	void Update() {
		/*if (fadeStarted) {
			fadeStarted = false;
		} else {
			targetAlpha = 1.0f;
		}*/
	}

	public void startFade() {
		//fadeStarted = true;
		targetAlpha = 0.3f;
		this.tag = "Fading";
	}

	public void endFade() {
		//fadeStarted = false;
		targetAlpha = 1.0f;
	}

	public void doFade() {
		if (currentAlpha < targetAlpha && (targetAlpha - currentAlpha) >= changeSpeed/2.0f) {
			currentAlpha += changeSpeed;
			
			for (int i = 0; i < renderer.materials.Length; i++) {
				Color c = renderer.materials[i].color;
				c.a = currentAlpha;
				renderer.materials[i].color = c;
			}
			
		} else if (currentAlpha > targetAlpha && (currentAlpha - targetAlpha) >= changeSpeed/2.0f) {
			currentAlpha -= changeSpeed;
			
			for (int i = 0; i < renderer.materials.Length; i++) {
				Color c = renderer.materials[i].color;
				c.a = currentAlpha;
				renderer.materials[i].color = c;
			}
		}
		
		if (currentAlpha >= (1.0f - changeSpeed/2.0f)) {
			currentAlpha = 1.0f;
			this.tag = "";
			for (int i = 0; i < renderer.materials.Length; i++) {
				//Debug.Log (originalShaders[i].name);
				renderer.materials[i].shader = originalShaders[i];
			}
		} else {
			Shader shader = Shader.Find ("Transparent/Diffuse");
			for (int i = 0; i < renderer.materials.Length; i++) {
				renderer.materials[i].shader = shader;
			}
		}
	}
}
