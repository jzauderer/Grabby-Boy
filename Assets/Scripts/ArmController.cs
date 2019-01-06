using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmController : MonoBehaviour
{
	private Vector3 mousePos;

    void Update()
    {
    	if(Input.GetKey(KeyCode.Escape))
    	{
			Application.Quit();
		}

		if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    	mousePos = Input.mousePosition;

    	//Control movement of the arm with an offset
    	float newX = mousePos.x/Screen.width;
    	transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    	//Control rotation of the arm
    	Vector3 rotationPos = new Vector3(mousePos.y/Screen.height * -55f + 105, 0, mousePos.x/Screen.width * -85f + 50);
    	transform.rotation = Quaternion.Euler(rotationPos);
    }
}
