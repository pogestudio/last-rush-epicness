using UnityEngine;
using System.Collections;
/// <summary>
/// This class can instantiate a weapon, if you know what kind of weapon you want.
/// </summary>
public class LootFactory : MonoBehaviour
{

    public GameObject machineGun;
    public GameObject handGun;
    public GameObject gatling;
    public GameObject shotGun;
    public GameObject sniper;


    private static LootFactory sharedInstance;

    public static LootFactory sharedFactory()
    {
        if (sharedInstance == null)
        {
            GameObject placeholder = GameObject.Find("LootFactory");

            if (placeholder == null)
            {
                Debug.Log("SharedFactory is being called AFTER THE SCEENE IS CLOSED (because monsters are killeD)! fix it");
                return null;
            }


            sharedInstance = placeholder.GetComponent<LootFactory>() as LootFactory;
        }
        return sharedInstance;
    }

    public GameObject createLoot(WeaponTypes typeOfWeapon, Transform monsterLocation)
    {
        GameObject instantiatedWeapon = null;
        switch (typeOfWeapon)
        {
            case WeaponTypes.MachineGun:
                {
                    instantiatedWeapon = Instantiate(machineGun, monsterLocation.position, monsterLocation.rotation) as GameObject;
                    break;
                }
            case WeaponTypes.HandGun:
                {
                    instantiatedWeapon = Instantiate(handGun, monsterLocation.position, monsterLocation.rotation) as GameObject;
                    break;
                }
            case WeaponTypes.Gatling:
                {
                    instantiatedWeapon = Instantiate(gatling, monsterLocation.position, monsterLocation.rotation) as GameObject;
                    break;
                }
            case WeaponTypes.ShotGun:
                {
                    instantiatedWeapon = Instantiate(shotGun, monsterLocation.position, monsterLocation.rotation) as GameObject;
                    break;
                }
            case WeaponTypes.SniperRifle:
                {
                    instantiatedWeapon = Instantiate(sniper, monsterLocation.position, monsterLocation.rotation) as GameObject;
                    break;
                }


            default:
                {
                    Debug.Log("We have more typesOfWeapons in game than in LootFactory!!");
                    break;
                }
        }
        return instantiatedWeapon;
    }

}

