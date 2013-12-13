using UnityEngine;
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

		private bool shouldShowInfo = true;

		public const int MAX_PLAYERS = 32;
		public const int PORT = 12123;

		private NetworkState state = NetworkState.NONE;
		private string textAdress = "127.0.0.1";

		private static int seed = 0;

		private static NetworkManager instance;
		
		public GUISkin toolTipSkin;
	

		public static bool offlineMode ()
		{
				return (instance == null);
		}

		public static NetworkManager get ()
		{
				return instance;
		}

		public static int getSeed ()
		{
				return seed;
		}

		void Awake ()
		{
				if (instance != null)
						throw new UnityException ("NetworkManager should only be attached to one object! This object will be kept beetween scenes.");
				else {
						instance = this;
						Object.DontDestroyOnLoad (gameObject);
				}
		}

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		[RPC]
		void StartGame (int sharedSeed)
		{
				seed = sharedSeed;
				Debug.Log ("Game Start!");
				Application.LoadLevel ("Scene_01");
				state = NetworkState.PLAYING;
		}


		void OnGUI ()
		{
		
		

				if (state == NetworkState.NONE) {
						if (GUI.Button (new Rect (10, 30, 50, 30), "HOST")) {
								Network.InitializeServer (MAX_PLAYERS, PORT, false);
								//Network.Connect("127.0.0.1", PORT);
								state = NetworkState.HOST;
						}
						textAdress = GUI.TextField (new Rect (10, 60, 200, 20), textAdress, 16);
						if (GUI.Button (new Rect (110, 80, 100, 30), "CONNECT")) {
								NetworkConnectionError status = Network.Connect (textAdress, PORT);
								if (status == NetworkConnectionError.NoError) {
										state = NetworkState.CONNECTED;
								}
						}
				} else if (state == NetworkState.HOST) {
						GUI.Label (new Rect (10, 10, 1000, 20), "connect to " + Network.player.ipAddress);
						GUI.Label (new Rect (10, 30, 1000, 20), "Other players connected: " + Network.connections.Length);

						if (GUI.Button (new Rect (10, 60, 50, 30), "START")) {
								shouldShowInfo = false;
								int randomSeed = Random.Range (0, int.MaxValue);
								Network.maxConnections = Network.connections.Length;
								networkView.RPC ("StartGame", RPCMode.All, randomSeed);
						}
				}
				
				if (shouldShowInfo)
						typeOutInfo ();

		}
    
		void typeOutInfo ()
		{
				string info = "Go north and seek out the end boss!\n\nPick up weapon on ground with E.\n\nEach weapon is connected to a skill, do damage to increase skill level.\n\nPress TAB to see the weapon skills connected to each weapon.\n\nRed glow = less damage per second than current weapon\nGreen glow = more damage per second than current weapon";
				GUI.Box (new Rect (10, 110, 300, 300), info, toolTipSkin.box);
				
		
		}
}
