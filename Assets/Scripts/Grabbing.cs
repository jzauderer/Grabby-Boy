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
    private Vector3 offsetAngle;
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

    //This catches cases where the grabbed object leaves our grip
    //without us letting go, like if they get knocked out by another object
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Grabbable"))
        {
            if(other.transform.parent.parent != null)
            {
                other.transform.parent.parent.GetComponent<EnemyController>().grabbed = false;
                other.transform.parent.parent.GetComponent<EnemyController>().flung = true;
            }
            else
            {
                other.transform.parent.GetComponent<Rigidbody>().useGravity = true;
            }
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

                    //Multiplyers for the different directions for throwing
                    Vector3 modifiedForce = new Vector3(launchDirection.x/1.8f, launchDirection.y/5.0f, Mathf.Abs(launchDirection.z)*8.0f);

                    Vector3 finalForce = modifiedForce*tossSpeedMult;

                    //The force of the throw is capped depending on what is being thrown
                    if(other.transform.parent.parent != null)
                    {
                        while(finalForce.magnitude > 80.0f)
                        {
                            finalForce = finalForce * 0.8f;
                        }
                    }
                    else
                    {
                        while(finalForce.magnitude > 40.0f)
                        {
                            finalForce = finalForce * 0.8f;
                        }
                    }

                    otherRigidBody[0].velocity = finalForce;
                }
                else
                {
                    otherRigidBody[0].velocity = Vector3.zero;
                    otherRigidBody[0].angularVelocity = Vector3.zero;
                }

                //If the object is an enemy, let it know it has been grabbed
                if(other.transform.parent.parent != null) //Will only be true on enemies
                {
                    other.transform.parent.parent.GetComponent<EnemyController>().grabbed = false;
                    other.transform.parent.parent.GetComponent<EnemyController>().flung = true;
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
    				offset = (other.transform.position - transform.position)/5;
    			}
    			other.transform.position = transform.TransformPoint(offset);
                other.transform.rotation = transform.rotation;

                if(other.transform.parent.parent != null) //Will only be true on enemies
                {
                    other.transform.parent.parent.GetComponent<EnemyController>().grabbed = true;
                    other.transform.parent.parent.GetComponent<EnemyController>().flung = false;
                }

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
