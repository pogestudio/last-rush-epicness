﻿using UnityEngine;
using System.Collections;

enum NetworkState
{
    NONE,
    HOST,
    CONNECTED,
    PLAYING
}

public class NetworkManager : MonoBehaviour
{
    public const int MAX_PLAYERS = 32;
    public const int PORT = 123452;

    private NetworkState state = NetworkState.NONE;
    private string textAdress = "127.0.0.1";

    private static NetworkManager instance;

    public static bool offlineMode()
    {
        return (instance == null);
    }
    
    public static NetworkManager get()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
            throw new UnityException("NetworkManager should only be attached to one object! This object will be kept beetween scenes.");
        else
        {
            instance = this;
            Object.DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [RPC]
    void StartGame()
    {
        Debug.Log("Game Start!");
        Application.LoadLevel("Scene_01");
        state = NetworkState.PLAYING;
    }

    void OnGUI()
    {
        if (state == NetworkState.NONE)
        {
            if (GUI.Button(new Rect(10, 30, 50, 30), "HOST"))
            {
                Network.InitializeServer(MAX_PLAYERS, 123452, false);
                state = NetworkState.HOST;
            }
            textAdress = GUI.TextField(new Rect(10, 60, 200, 20), textAdress, 16);
            if (GUI.Button(new Rect(110, 80, 100, 30), "CONNECT"))
            {
                NetworkConnectionError status = Network.Connect(textAdress, PORT);
                if (status == NetworkConnectionError.NoError)
                {
                    state = NetworkState.CONNECTED;
                }
            }
        }
        else if (state == NetworkState.HOST)
        {
            if (GUI.Button(new Rect(10, 30, 50, 30), "START"))
            {
                networkView.RPC("StartGame",RPCMode.All);
            }
        }

    }
}
