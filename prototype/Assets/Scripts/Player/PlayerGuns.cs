using UnityEngine;
using System.Collections;

public class PlayerGuns : MonoBehaviour
{
	public float MaximumPickupRange = 3;

    public GameObject currentGun;

    void Awake()
    {
        if (currentGun == null)
            Debug.Log("PlayerGuns.currentGun should be set if the player is carrying a gun!");
    }

    void Start()
    {
        pickWeapon(currentGun);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			//looking for the closest weapon within range
			Transform closestWeapon = null;
			float bestDistance = MaximumPickupRange;
			foreach (Transform otherWeapon in WeaponManager.get().WeaponsContainer.transform)
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
		
			//set new weapon transform so it's in player's hand
			newWeapon.transform.parent = this.transform;
			newWeapon.transform.localPosition = Vector3.zero;
			newWeapon.transform.localRotation = Quaternion.identity;
			newWeaponScript.Mode = WeaponMode.HAND;

			//drop current weapon
            if (currentGun != null && newWeapon!= currentGun)
            {
                AbstractWeapon currentGunScript = currentGun.GetComponent<AbstractWeapon>();
                WeaponManager.get().manage(currentGunScript);
            }

			currentGun = newWeapon;
	}
}
