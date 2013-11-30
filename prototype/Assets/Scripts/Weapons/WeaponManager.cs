using UnityEngine;
using System.Collections;

public class WeaponManager {

    private static WeaponManager uniqueInstance = null;

    private GameObject weaponsContainer;
    public GameObject WeaponsContainer
    {
        get { return weaponsContainer; }
    }

    /// <summary>
    /// Allows to acces the unique instance of WeaponManager (singleton).
    /// </summary>
    /// <returns>WeaponManager singleton</returns>
    public static WeaponManager get()
    {
        if (uniqueInstance == null)
            uniqueInstance = new WeaponManager();

        return uniqueInstance;
    }

    private WeaponManager()
    {
        //create WeaponRagdollContainer
        weaponsContainer = new GameObject();
        weaponsContainer.name = "weaponsContainer";
    }

    public void manage(AbstractWeapon weapon)
    {
        weapon.transform.parent = WeaponManager.get().WeaponsContainer.transform;
        weapon.GetComponent<AbstractWeapon>().Mode = WeaponMode.RAGDOLL;
    }

}
