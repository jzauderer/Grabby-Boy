using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
	private Healthbar hpScript;
	private EnemyController enemyState;

	void Start()
	{
		//Finds the healthbar object and pulls the script from it
		hpScript = GameObject.FindGameObjectsWithTag("Healthbar")[0].GetComponent<Healthbar>();
		enemyState = transform.parent.GetComponent<EnemyController>();
	}

    void OnCollisionEnter(Collision other)
    {
    	float impactSpeed =  other.relativeVelocity.magnitude;

    	//Only take damage from projectiles when not grabbed or flung
    	//Bonus damage if the collision is from a projectile
    	if(other.gameObject.CompareTag("Projectile") && !enemyState.grabbed && !enemyState.flung)
    	{
    		hpScript.changeHealth(impactSpeed*-1);
    	}
    	//Don't take damage when held
    	else if(!enemyState.grabbed)
    	{
    		hpScript.changeHealth(impactSpeed*-0.75f);
    	}

    	//Declare enemy dead if health is reduced to 0
    	if(hpScript.getHealth()  == 0)
    	{
    		enemyState.dead = true;
    	}
    	
    }
}
