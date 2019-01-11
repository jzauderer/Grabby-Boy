using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbox : MonoBehaviour
{
	public GameObject myPhysicalObject;
	public float magnetPower;

	private bool touchingHand;

	void Start()
	{
		touchingHand = false;
	}
    
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("HandGrabBox"))
		{
			touchingHand = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("HandGrabBox"))
		{
			touchingHand = false;
		}
	}

    void FixedUpdate()
    {
    	//Magnetism
    	if(Input.GetMouseButton(1))
    	{
    		GameObject handBox = GameObject.FindGameObjectsWithTag("HandGrabBox")[0];
    		Vector3 magnetVector = handBox.transform.position - transform.position;
    		//Magnetism is stronger on the y axis to prevent them 
    		//from just lying on the floor in front of you.
    		magnetVector = new Vector3(magnetVector.x, magnetVector.y * 6.0f, magnetVector.z);
    		myPhysicalObject.GetComponent<Rigidbody>().AddForce(magnetVector * magnetPower * Time.deltaTime);
    	}

    	//Turn gravity off when the object is held and makes the grabbox properly follow the object
    	if(Input.GetMouseButton(0) && touchingHand)
    	{
    		//Instead of the grabbox following the object, now the object should
    		//follow the grabbox, as that's the one being manipulated here
    		myPhysicalObject.transform.position = transform.position;

            //For throwing projectiles, which do not handle their own gravity
    		if(myPhysicalObject.transform.parent == null)
    		{
    			myPhysicalObject.GetComponent<Rigidbody>().useGravity = false;
    		}
    	}
    	else
    	{
    		transform.position = myPhysicalObject.transform.position;
            
    		if(myPhysicalObject.transform.parent == null)
    		{
    			myPhysicalObject.GetComponent<Rigidbody>().useGravity = true;
    		}
    	}
    }
}
