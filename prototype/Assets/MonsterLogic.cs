﻿using UnityEngine;
using System.Collections;

/*

This is monster logic, and decides all monster related behaviour.
health - the health of the monster

movingSpeed - decides how fast the monster is moving


GUIDING SHOULD BE REFACTORED to a seperate component. 

*/

public class MonsterLogic : MonoBehaviour
{

    public int health = 50;
    public float movingSpeed;
    private GameObject player;

    private void die ()
    {
        Debug.Log ("Monster should die");
        gameObject.SetActive (false);
    }


    public void applyDamage (int damage)
    {
        health -= damage;
        if (health <= 0) {
            die ();
        }
    }

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag ("Player"); 
        if (!player) 
            Debug.Log ("ERROR could not find Player!"); 
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (!player) 
            return; 


        //WALKING FUNCTION
        //float distance = Vector3.Distance( player.transform.position, transform.position);
        Vector3 delta = player.transform.position - transform.position;
        delta.Normalize ();
        float moveSpeed = movingSpeed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
        transform.LookAt (player.transform);
    }
}
