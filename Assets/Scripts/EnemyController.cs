﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Time between enemy movements
    public float moveStay;

    //How long the enemy is frozen in place after moving
    public float freezeDur;

    //Timer to prevent the enemy from lying on the floor
    //endlessly in the case of very slight movements
    public float getUpDelay;

    public bool grabbed;
    public bool flung;
    public bool dead;

    private Transform torso;
    private Transform armR;
    private Transform armL;
    private Transform legR;
    private Transform legL;

    private float getUpTimer;

    private float moveTimer;

    private float freezeTimer;
    private bool freezing;
    
    void Start()
    {
        torso = transform.GetChild(1);
        armR = transform.GetChild(2);
        armL = transform.GetChild(3);
        legR = transform.GetChild(4);
        legL = transform.GetChild(5);

        moveTimer = 0.0f;
        freezeTimer = 0.0f;
        freezing = false;
        grabbed = false;
        flung = false;
        dead = false;

        getUpTimer = 0f;
    }

    void Update()
    {
    	if(!grabbed && !flung && !dead)
    		moveTimer += Time.deltaTime;

    	//After every interval, move to a random spot in the bounds
        if(moveTimer > moveStay)
        {
            Vector3 newLoc = new Vector3(Random.Range(-7f,7f), Random.Range(2.3f,5f), Random.Range(15.5f,22f));
        	torso.position = newLoc;
        	torso.rotation = Quaternion.Euler(new Vector3(0, 0 ,0));
        	freezing = true;
        	moveTimer = 0.0f;
        }

        //The enemy stands still for a brief duration after moving
        if(freezing)
        {
        	foreach(Transform child in transform)
    		{
    			torso.rotation = Quaternion.Euler(new Vector3(0, 0 ,0));
    			child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    			child.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    		}

        	freezeTimer += Time.deltaTime;
        	if(freezeTimer > freezeDur)
        	{
        		freezing = false;
        		freezeTimer = 0.0f;
        	}
        }
        
        //Enemy should behave differently when grabbed
        if(grabbed)
        {
            getUpTimer = 0;
        	moveTimer = 0;
        	//Remove gravity
        	foreach(Transform child in transform)
    		{
    			child.gameObject.GetComponent<Rigidbody>().useGravity = false;
    		}
        }

        //Enemy should use gravity after being thrown
        if(flung)
        {
            getUpTimer += Time.deltaTime;
            if(getUpTimer > getUpDelay)
            {
                //Failsafe for if they get stuck
                flung = false;
                moveTimer = moveStay;
                foreach(Transform child in transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
                getUpTimer = 0;
            }
            else
            {
                moveTimer = 0;
                foreach(Transform child in transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }

        //Enemy should resume movement after reaching a halt
        if(flung && torso.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
        	flung = false;
        	moveTimer = moveStay;

        	foreach(Transform child in transform)
    		{
    			child.gameObject.GetComponent<Rigidbody>().useGravity = false;
    		}
        }
    }
}
