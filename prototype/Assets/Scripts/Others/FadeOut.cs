using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {

	private Shader[] originalShaders;

	private float targetAlpha = 1.0f;
	private float currentAlpha = 1.0f;

	private float changeSpeed = 0.05f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			GameObject child = transform.GetChild(i).gameObject;
			if (child.GetComponent<Renderer>() && !child.GetComponent<FadeOut>()) {
				child.AddComponent<FadeOut>();
			}
		}

		if (!renderer) {
			return;
		}

		originalShaders = new Shader[renderer.materials.Length];

		for (int i = 0; i < renderer.materials.Length; i++) {
			originalShaders[i] = renderer.materials[i].shader;
		}
	}

	public void startFade() {
		targetAlpha = 0.3f;
		this.tag = "Fading";

		FadeOut[] childFades = GetComponentsInChildren<FadeOut>();
		if (childFades.Length > 0) {
			foreach (FadeOut childFade in childFades) {
				childFade.targetAlpha = 0.3f;
				childFade.tag = "Fading";
			}
		}
	}

	public void endFade() {
		targetAlpha = 1.0f;

		FadeOut[] childFades = GetComponentsInChildren<FadeOut>();
		if (childFades.Length > 0) {
			foreach (FadeOut childFade in childFades)
				childFade.targetAlpha = 1.0f;
		}
	}

	public void doFade() {
		FadeOut[] childFades = GetComponentsInChildren<FadeOut>();

		if (renderer)
			doActualFade();

		if (childFades.Length > 0) {
			foreach (FadeOut childFade in childFades)
				if (childFade.GetComponent<Renderer>())
					childFade.doActualFade();
		}
	}

	private void doActualFade() {
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
