using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbox : MonoBehaviour
{
	public GameObject myPhysicalObject;

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
    	if(Input.GetMouseButton(0) && touchingHand)
    	{
    		myPhysicalObject.transform.position = transform.position;
    		myPhysicalObject.GetComponent<Rigidbody>().useGravity = false;
    	}
    	else
    	{
    		transform.position = myPhysicalObject.transform.position;
    		myPhysicalObject.GetComponent<Rigidbody>().useGravity = true;
    	}
    }
}
