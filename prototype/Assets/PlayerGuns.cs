using UnityEngine;
using System.Collections;

public class PlayerGuns : MonoBehaviour
{

    private static GameObject ragdollWeapons;
    private static GameObject currentGun;

    void Awake()
    {
        ragdollWeapons = GameObject.Find("RagdollWeapons");
        currentGun = transform.FindChild("Gun").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            dropCurrentWeapon();
    }

    public void dropCurrentWeapon()
    {

            currentGun.transform.parent = ragdollWeapons.transform;
            currentGun.GetComponent<AbstractWeapon>().Mode = AbstractWeapon.WeaponMode.RAGDOLL;
    }
}
