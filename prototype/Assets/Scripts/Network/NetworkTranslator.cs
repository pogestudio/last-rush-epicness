using UnityEngine;
using System.Collections;

public class NetworkTranslator 
{

    public static NetworkViewID ToId(GameObject obj)
    {
        if (obj == null || obj.networkView == null)
        {
            Debug.Log("trying to get network id of a non syncronized or null object.");
        }

        return obj.networkView.viewID;
    }

    public static GameObject ToInstance(NetworkViewID networkId)
    {
        return NetworkView.Find(networkId).gameObject;
    }

}
