using UnityEngine;
using System.Collections;


public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public WeaponTypes startWeapon;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 1) as GameObject;   //Fallback for single player

		audio.Play ();
        //give the player his start weapon
        GameObject weaponInstance = LootHandler.sharedHandler().createLootNoMatterWhat(startWeapon, transform);
        player.GetComponent<PlayerWeapons>().pickWeapon(weaponInstance);
            

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TopDownCamera>().target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
