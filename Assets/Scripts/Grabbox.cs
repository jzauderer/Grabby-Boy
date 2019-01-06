using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbox : MonoBehaviour
{
	public GameObject myPhysicalObject;
	public float magnetPower;

	private bool touchingHand;
	private GameObject objParent;

	void Start()
	{
		touchingHand = false;
		objParent = myPhysicalObject.transform.parent.gameObject;
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
    		myPhysicalObject.GetComponent<Rigidbody>().AddForce(magnetVector * magnetPower);
    	}

    	//Turn gravity off when the object is held and makes the grabbox properly follow the object
    	if(Input.GetMouseButton(0) && touchingHand)
    	{
    		myPhysicalObject.transform.position = transform.position;
    		foreach(Transform child in objParent.transform)
    		{
    			child.gameObject.GetComponent<Rigidbody>().useGravity = false;
    		}
    	}
    	else
    	{
    		transform.position = myPhysicalObject.transform.position;
    		foreach(Transform child in objParent.transform)
    		{
    			child.gameObject.GetComponent<Rigidbody>().useGravity = true;
    		}
    	}
    }
}
