using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Time between enemy movements
    public float moveStay;

    //How long the enemy is frozen in place after moving
    public float freezeDur;

    private Transform torso;
    private Transform armR;
    private Transform armL;
    private Transform legR;
    private Transform legL;

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
    }

    void Update()
    {
    	//After every interval, move to a random spot in the bounds
        moveTimer += Time.deltaTime;
        if(moveTimer > moveStay)
        {
            Vector3 newLoc = new Vector3(Random.Range(-7f,7f), Random.Range(2f,3f), Random.Range(5.5f,24f));
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
    }
}
