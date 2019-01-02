using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    private bool grabOn;
    private bool objectGrabbed;
    private Vector3 offset;

    void Start()
    {
        grabOn = false;
        objectGrabbed = false;
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
    	}
    }

    void OnTriggerStay(Collider other){
    	if(other.gameObject.CompareTag("Grabbable"))
    	{
    		if(grabOn)
    		{
    			if(!objectGrabbed)
    			{
    				objectGrabbed = true;
    				offset = (other.transform.position - transform.position)/3;
    			}
    			other.transform.position = transform.position + offset;
    		}
    	}
    }
}
