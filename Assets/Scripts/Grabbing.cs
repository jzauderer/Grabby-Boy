using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    //How many frames you have to hold an object in order for it to
    //receive thrown momentum
    public int tossTime;
    public int tossSpeedMult;

    private bool grabOn;
    private bool objectGrabbed;
    private Vector3 offset;
    private Vector3[] positions;
    private int tossTimeIndex;

    void Start()
    {
        grabOn = false;
        objectGrabbed = false;
        positions = new Vector3[tossTime];
        tossTimeIndex = 0;
    }

    void Update()
    {
    	if(Input.GetMouseButton(0))
    	{
    		grabOn = true;
    	}
    	else
    	{
    		grabOn = false;
            objectGrabbed = false;
    	}
    }

    void OnTriggerStay(Collider other){
    	if(other.gameObject.CompareTag("Grabbable"))
    	{
            //When the player releases the object
            if(Input.GetMouseButtonUp(0))
            {
                //Get rigidbody of the thrown object
                Rigidbody[] otherRigidBody = other.GetComponentsInParent<Rigidbody>();
                //If held long enough, throw it. Otherwise, let it drop
                if(tossTimeIndex >= tossTime-1)
                {
                    Vector3 launchDirection = positions[tossTime/2] - positions[0];
                    Vector3 resultantForce = new Vector3(launchDirection.x/2.0f, launchDirection.y/5.0f, launchDirection.z*3.0f);
                    otherRigidBody[0].velocity = resultantForce*tossSpeedMult;
                }
                else
                {
                    otherRigidBody[0].velocity = Vector3.zero;
                    otherRigidBody[0].angularVelocity = Vector3.zero;
                }
                //Reset array of positions for grabbed object
                positions = new Vector3[tossTime];
                tossTimeIndex = 0;
            }
            //If left click is held
    		else if(grabOn)
    		{
                //Set offset distance upon first grabbing
    			if(!objectGrabbed)
    			{
    				objectGrabbed = true;
    				offset = (other.transform.position - transform.position)/3;
    			}
    			other.transform.position = transform.position + offset;

                /*
                positions stores the positions of the object over the last
                x frames, x being tossTime. If positions is not full, we have 
                not held the object long enough to throw it with force
                */ 
                if(tossTimeIndex < tossTime)
                {
                    positions[tossTimeIndex] = other.transform.position;
                    tossTimeIndex++;
                }
                else
                {
                    for(int i = 1; i < tossTimeIndex; i++)
                    {
                        positions[i-1] = positions[i];
                    }
                    positions[tossTime-1] = other.transform.position;
                }
    		}
    	}
    }
}
