using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour
{
    public float MaximumPickupRange = 3;

    private GameObject currentWeapon;
    private AbstractWeapon currentWeaponScript;
    private static PlayerWeapons mainPlayerScript;

    void Awake()
    {
        GameObject mainPlayer = GameObject.FindGameObjectWithTag("Player");
        if (mainPlayer == null)
            Debug.LogError("Unnable to find main player!");

        mainPlayerScript = mainPlayer.GetComponent<PlayerWeapons>();
        if (mainPlayerScript == null)
            Debug.LogError("Unnable to find main player weapon script!");
    }

    void Start()
    {
    }

    public static PlayerWeapons getMainPlayerScript()
    {
        return mainPlayerScript;
    }

    public static GameObject getMainPlayerWeapon()
    {
        return mainPlayerScript.currentWeapon;
    }

    public static WeaponTypes getMainPlayerWeaponType()
    {
        return mainPlayerScript.currentWeaponScript.Type;
    }

    public static AbstractWeapon getMainPlayerWeaponScript()
    {
        if (!mainPlayerScript)
            return null;

        return mainPlayerScript.currentWeaponScript;
    }


    // Update is called once per frame
    void Update()
    {
        if (networkView.isMine)
        {
            //WEAPON FIRING
            if (currentWeapon)
            {
                if (Input.GetButtonDown("Fire1"))
                    currentWeaponScript.triggerDown();
                else if (Input.GetButtonUp("Fire1"))
                    currentWeaponScript.triggerUp();

                if (Input.GetButton("Fire1"))
                    currentWeaponScript.triggerHold();
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
        if (currentWeapon != null && newWeapon != currentWeapon)
        {
            AbstractWeapon oldGunScript = currentWeapon.GetComponent<AbstractWeapon>();
            WeaponManager.get().manage(oldGunScript);
        }

        currentWeapon = newWeapon;
        currentWeaponScript = newWeaponScript;

        //replicate
        networkView.RPC("replicateWeapon", RPCMode.Others, (int)newWeaponScript.Type);
    }


    //TODO: improve this when the loot is synchronized
    /// <summary>
    /// Placeholder RPC to set fake weapons on replicated players
    /// </summary>
    /// <param name="type"></param>
    [RPC]
    private void replicateWeapon(int weaponTypeOrdinal)
    {
        if (!networkView.isMine)
        {    //make sure that this is a replicate
            if (currentWeapon)
            {
                Destroy(currentWeapon);
            }
            WeaponTypes type = (WeaponTypes)weaponTypeOrdinal;
            currentWeapon = LootHandler.sharedHandler().createLootNoMatterWhat(type, transform);
            currentWeapon.transform.parent = this.transform;
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeapon.GetComponent<AbstractWeapon>().Mode = WeaponMode.HAND;
        }
    }


}
