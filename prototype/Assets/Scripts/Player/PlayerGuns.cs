using UnityEngine;
using System.Collections;

public class PlayerGuns : MonoBehaviour
{
	public float MaximumPickupRange = 3;

    private GameObject ragdollWeapons;
    public GameObject currentGun;

    void Awake()
    {
        if (currentGun == null)
            Debug.Log("PlayerGuns.currentGun should be set if the player is carrying a gun!");

        ragdollWeapons = GameObject.FindGameObjectWithTag("RagdollWeaponsContainer");
        currentGun.GetComponent<AbstractWeapon>().Mode = WeaponMode.HAND;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			//looking for the closest weapon within range
			Transform closestWeapon = null;
			float bestDistance = MaximumPickupRange;
			foreach (Transform otherWeapon in ragdollWeapons.transform)
			{
				float distance = Vector3.Distance(transform.position, otherWeapon.transform.position);
				if (distance < bestDistance)
				{
					bestDistance = distance;
					closestWeapon = otherWeapon;
				}
			}


			//if found, switch
			if (closestWeapon != null)
			{
				pickWeapon(closestWeapon.gameObject);
			}
		}
    }

	private void pickWeapon(GameObject newWeapon)
	{
		AbstractWeapon newWeaponScript = newWeapon.GetComponent<AbstractWeapon>();
		if (newWeaponScript.Mode == WeaponMode.RAGDOLL)
		{
			//set new weapon transform so that it's handled where the old one was
			newWeapon.transform.parent = this.transform;
			newWeapon.transform.localPosition = Vector3.zero;
			newWeapon.transform.localRotation = Quaternion.identity;
			newWeaponScript.Mode = WeaponMode.HAND;

			//drop current weapon
			currentGun.transform.parent = ragdollWeapons.transform;
			currentGun.GetComponent<AbstractWeapon>().Mode = WeaponMode.RAGDOLL;

			currentGun = newWeapon;
			
		}
	}

    private void dropCurrentWeapon()
    {
		
    }
}
