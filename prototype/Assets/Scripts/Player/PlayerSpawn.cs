using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = Network.Instantiate(playerPrefab, transform.position, transform.rotation, 1) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
