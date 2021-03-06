﻿using UnityEngine;
using System.Collections;

/*

This is player logic, and decides all player related behaviour.
health - the health of the player

*/

public class PlayerLogic : MonoBehaviour
{

    public int health = 3;

    private void die ()
    {
        Debug.Log ("Player should die");
        gameObject.SetActive (false);
    }

    public void applyDamage (int damage)
    {
        health -= damage;
        if (health <= 0) {
            die ();
        }
    }

}
