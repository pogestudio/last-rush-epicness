using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour
{
	public float MaximumPickupRange = 3;

    private GameObject currentGun;
    private AbstractWeapon currentGunScript;

    void Awake()
    {
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
            //WEAPON FIRING
            if (currentGun)
            {
                if (Input.GetButtonDown("Fire1"))
                    currentGunScript.triggerDown();
                else if (Input.GetButtonUp("Fire1"))
                    currentGunScript.triggerUp();

                if (Input.GetButton("Fire1"))
                    currentGunScript.triggerHold();
            }
            //WEAPON PICKING
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
    }

	public void pickWeapon(GameObject newWeapon)
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
                AbstractWeapon oldGunScript = currentGun.GetComponent<AbstractWeapon>();
                WeaponManager.get().manage(oldGunScript);
            }

			currentGun = newWeapon;
            currentGunScript = newWeaponScript;
	}

}
