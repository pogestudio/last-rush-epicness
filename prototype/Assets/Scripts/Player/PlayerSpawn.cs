using UnityEngine;
using System.Collections;


public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public WeaponTypes startWeapon;
    public GameObject[] models;
    GameObject player;
    private int playersToWait;

    private static PlayerSpawn instance;

    public static PlayerSpawn get()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
            Debug.LogError("multiple PlayerSpawns!");

        instance = this;
        playersToWait = Network.connections.Length + 1;  //server is counted not a connection
    }


    // game start should be syncronized into NetworkManager but for some reason RPC are failling for persistent objects.
    // So this is a workaround :)
    public void imReady()
    {
        networkView.RPC("playerReadyRPC", RPCMode.AllBuffered);
    }

    [RPC]
    public void playerReadyRPC()
    {
        playersToWait--;
        if (playersToWait == 0)
        {
            spawnPlayer();
        }
    }


    // Use this for initialization
    void Start()
    {
        imReady();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void spawnPlayer()
    {
        player = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 1) as GameObject;   //Fallback for single player

        audio.Play();
        //give the player his start weapon
        GameObject weaponInstance = LootHandler.sharedHandler().createLootNoMatterWhat(startWeapon, transform);
        player.GetComponent<PlayerWeapons>().pickWeapon(weaponInstance);


        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TopDownCamera>().target = player.transform;

        System.Random rnd = new System.Random();    //unity random has been seeded
        int randomModelIndex = rnd.Next(0, models.Length);
        networkView.RPC("initPlayerReplica", RPCMode.AllBuffered, NetworkTranslator.ToId(player), randomModelIndex);
    }

    [RPC]
    private void initPlayerReplica(NetworkViewID id, int modelIndex)
    {
        GameObject player = NetworkTranslator.ToInstance(id);
        GameObject model = Instantiate(models[modelIndex]) as GameObject;
        model.transform.parent = player.transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.name = "model";
    }

    void OnGUI()
    {
        if (playersToWait != 0)
        {
            string text = "Waiting for " + playersToWait + " players to connect...";
            GUI.Label(new Rect(10, 30, 100, 20), text);
        }
    }
}
