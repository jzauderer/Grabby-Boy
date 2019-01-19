using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
	public AudioClip hit1;
	public AudioClip hit2;
	public AudioClip hit3;

	private AudioSource weakened;
	private Healthbar hpScript;
	private EnemyController enemyState;
	private AudioClip[] hitSounds;

	void Start()
	{
		hitSounds = new AudioClip[]{hit1,hit2,hit3};
		weakened = GetComponent<AudioSource>();

		//Finds the healthbar object and pulls the script from it
		hpScript = GameObject.FindGameObjectsWithTag("Healthbar")[0].GetComponent<Healthbar>();
		enemyState = transform.parent.GetComponent<EnemyController>();
	}

    void OnCollisionEnter(Collision other)
    {
    	float impactSpeed =  other.relativeVelocity.magnitude;

    	//Only take damage from projectiles when not grabbed or flung
    	//Bonus damage if the collision is from a projectile
    	//Damage taken from projectile also scales to the part hit
    	if(other.gameObject.CompareTag("Projectile") && !enemyState.grabbed && !enemyState.flung)
    	{
    		enemyState.flung = true;
    		if(gameObject.CompareTag("Torso"))
    			hpScript.changeHealth(impactSpeed*-1.25f);
    		else if(gameObject.CompareTag("Head"))
    			hpScript.changeHealth(impactSpeed*-2.50f);
    		else
    			hpScript.changeHealth(impactSpeed*-0.5f);
    		weakened.Play();
    	}
    	//Don't take damage when held
    	//Only takes normal bludgeoning damage on head and torso
    	else if(enemyState.flung && (gameObject.CompareTag("Torso") || gameObject.CompareTag("Head")))
    	{
    		hpScript.changeHealth(impactSpeed*-0.3f);
    		
    		//Chooses one of the three hit sounds at random
    		AudioSource.PlayClipAtPoint(hitSounds[Random.Range(0,3)], transform.position);
    	}

    	//Declare enemy dead if health is reduced to 0
    	if(hpScript.getHealth()  == 0)
    	{
    		enemyState.dead = true;
    	}
    }
}
