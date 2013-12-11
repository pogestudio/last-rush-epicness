using UnityEngine;
using System.Collections;

public abstract class RaycastWeapon : AbstractWeapon {

	public LineRenderer lineRenderer;

	void Start() {
		//lineRenderer = new LineRenderer();
	}

	public override void fire ()
	{
		RaycastHit[] hits = Physics.RaycastAll(gunMuzzle.position, transform.forward);

		//Debug.DrawRay(gunMuzzle.position, transform.forward * 1000f, Color.red, 2f);

		// Need to sort by distance (closest first)
		hits = sortByDistance(hits);

		//SkillEffect[] effects = ghostProjectile.GetComponents<SkillEffect>();
		bool isPierce = false;

		foreach (RaycastHit hit in hits) {
			isPierce = false;

			GameObject target = hit.collider.gameObject;
			//Debug.Log ("Hit " + target.name);

			GameObject ghostProjectile = ProjectileFactory.sharedFactory().deliverProjectile(gunMuzzle, type, weaponDamage);
			ghostProjectile.SetActive(false);
			SkillEffect[] effects = ghostProjectile.GetComponents<SkillEffect>();

			foreach (SkillEffect effect in effects) {
				if (effect.targetIsEnemy(target) && !(effect is ShrapnelShot) && !(effect is PiercingShot)) {
					effect.doDamage(target);
					ghostProjectile.transform.position = hit.point;
					effect.createEffect(target);
				}

				if (effect is PiercingShot) {
					isPierce = true;
				}
			}

			LineRenderer tmp = GameObject.Instantiate(lineRenderer) as LineRenderer;
			tmp.SetVertexCount(2);
			tmp.SetWidth(0.1f, 0.1f);
			tmp.SetColors(Color.white, Color.white);
			tmp.SetPosition(0, gunMuzzle.position);
			tmp.SetPosition(1, hit.point);
			Destroy(tmp, 0.5f);


			// TODO: Shrapnel, multishot, homingshot

			if (!isPierce)
				break;

			Destroy (ghostProjectile);
		}

		StartCoroutine(flash ());
		if (audio)
			audio.Play ();
	}

	private RaycastHit[] sortByDistance(RaycastHit[] hits) {
		for (int i = 1; i < hits.Length; i++) {
			RaycastHit valueToInsert = hits[i];
			int holePos = i;
			while (holePos > 0 && valueToInsert.distance < hits[holePos - 1].distance) {
				hits[holePos] = hits[holePos - 1];
				holePos--;
			}
			hits[holePos]= valueToInsert;
		}

		return hits;
	}

	//shoot flash coroutine
	private IEnumerator flash()
	{
		gunMuzzle.light.enabled = true;
		yield return 0;
		gunMuzzle.light.enabled = false;
	}
}