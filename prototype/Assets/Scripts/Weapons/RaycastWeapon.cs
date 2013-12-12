using UnityEngine;
using System.Collections;

public abstract class RaycastWeapon : AbstractWeapon {

	private bool isMultishot = false;
	private bool isShrapnelShot = false;

	void Start() {
		//lineRenderer = new LineRenderer();
	}

	public override void fire ()
	{
		// Find if we have any special treatment effects

		GameObject ghostProjectile = ProjectileFactory.sharedFactory().deliverProjectile(gunMuzzle, type, weaponDamage, false);
		ghostProjectile.SetActive(false);

		// TODO: Homingshot

		SkillEffect[] effects = ghostProjectile.GetComponents<SkillEffect>();
		foreach (SkillEffect effect in effects) {
			if (effect is MultiShot && !isMultishot) {
				isMultishot = true;
				Quaternion originalRotation = transform.rotation;
				transform.Rotate(Vector3.up, -15.0f);
				fire();
				transform.rotation = originalRotation;
				transform.Rotate(Vector3.up, 15.0f);
				fire();
				transform.rotation = originalRotation;
				isMultishot = false;
			}
		}

		RaycastHit[] hits = Physics.RaycastAll(gunMuzzle.position, transform.forward);

		// Need to sort by distance (closest first)
		hits = sortByDistance(hits);


		bool isPierce = false;

		foreach (RaycastHit hit in hits) {
			isPierce = false;

			GameObject target = hit.collider.gameObject;
			//Debug.Log ("Hit " + target.name);

			effects = ghostProjectile.GetComponents<SkillEffect>();

			foreach (SkillEffect effect in effects) {
				if (effect.targetIsEnemy(target) && !(effect is ShrapnelShot) && !(effect is PiercingShot)) {
					effect.doDamage(target);
					ghostProjectile.transform.position = hit.point;
					effect.createEffect(target);
				}

				if (effect is PiercingShot) {
					isPierce = true;
				}

				if (effect.targetIsEnemy(target) && effect is ShrapnelShot && !isShrapnelShot) {
					Vector3 originalPosition = gunMuzzle.position;
					Quaternion originalRotation = transform.rotation;

					isShrapnelShot = true;
					isMultishot = true;
					for (int i = 0; i < 4; i++) {
						float rotationAngle;
						float factor = (float)i / 4f;

						if (factor < 0.5) {
							rotationAngle = -(135 - (90) * factor);
						} else {
							rotationAngle = 45 + 90 * factor;
						}

						transform.Rotate(Vector3.up, rotationAngle);
						gunMuzzle.position = hit.point;
						fire();
						transform.rotation = originalRotation;
					}
					isShrapnelShot = false;
					isMultishot = false;
					gunMuzzle.position = originalPosition;
				}
			}

            EffectFactory.sharedFactory().createSniperShot(gunMuzzle.position, hit.point);

			if (!isPierce)
				break;

			Destroy(ghostProjectile);

			ghostProjectile = ProjectileFactory.sharedFactory().deliverProjectile(gunMuzzle, type, weaponDamage , false);
			ghostProjectile.SetActive(false);
		}
        Destroy(ghostProjectile);

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