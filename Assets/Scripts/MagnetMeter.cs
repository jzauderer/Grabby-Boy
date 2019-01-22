using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetMeter : MonoBehaviour
{
	public GameObject handBox;
	public GameObject overlay;
	public GameObject grabIndicator;
	public Sprite normal;
	public Sprite angery;

	//The duration the "Grab Now" indicator stays up
	//Also the duration it stays gone for
	public float flashCooldown;

	private Grabbing grabScript;
	private EnemyController enemyScript;
	private float flashTimer;

    void Start()
    {
        grabScript = handBox.GetComponent<Grabbing>();

        //Pull script from enemy
        GameObject enemyObj = GameObject.FindGameObjectsWithTag("Enemy")[0];
        enemyScript = enemyObj.GetComponent<EnemyController>();

        //We want the indicator to appear pretty much immediately
        flashTimer = flashCooldown - 0.001f;
    }

    void Update()
    {
    	//Magnetism Meter
    	//Change icon to indicate that the enemy is grabbable
    	if((enemyScript.grabbed || enemyScript.flung) && !enemyScript.dead)
    	{
    		gameObject.GetComponent<Image>().sprite = angery;
    		overlay.SetActive(true);

    		//"Grab Now" indicator
    		flashTimer += Time.deltaTime;

    		if(flashTimer > flashCooldown)
    		{
    			grabIndicator.SetActive(!grabIndicator.activeSelf);
    			flashTimer = 0.0f;
    		}
    	}
    	else
    	{
    		gameObject.GetComponent<Image>().sprite = normal;
    		overlay.SetActive(false);
    		flashTimer = flashCooldown - 0.001f;
    		grabIndicator.SetActive(false);
    	}

    	gameObject.GetComponent<Image>().fillAmount = grabScript.currentEnergy/grabScript.maxEnergy;
    }
}
