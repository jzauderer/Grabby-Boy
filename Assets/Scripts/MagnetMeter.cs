using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetMeter : MonoBehaviour
{
	public GameObject handBox;
	public GameObject overlay;
	public Sprite normal;
	public Sprite angery;

	private Grabbing grabScript;
	private EnemyController enemyScript;

    void Start()
    {
        grabScript = handBox.GetComponent<Grabbing>();

        //Pull script from enemy
        GameObject enemyObj = GameObject.FindGameObjectsWithTag("Enemy")[0];
        enemyScript = enemyObj.GetComponent<EnemyController>();
    }

    void Update()
    {
    	//Change icon to indicate that the enemy is grabbable
    	if(enemyScript.grabbed || enemyScript.flung)
    	{
    		gameObject.GetComponent<Image>().sprite = angery;
    		overlay.SetActive(true);
    	}
    	else
    	{
    		gameObject.GetComponent<Image>().sprite = normal;
    		overlay.SetActive(false);
    	}

    	gameObject.GetComponent<Image>().fillAmount = grabScript.currentEnergy/grabScript.maxEnergy;
    }
}
