using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
	private Vector3 mousePos;

    void Update()
    {
    	mousePos = Input.mousePosition;

    	//Control movement of the arm with an offset
    	float newX = mousePos.x/Screen.width + .3f;
    	float newY = mousePos.y/Screen.height + 1;
    	transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
