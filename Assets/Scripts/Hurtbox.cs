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
    	if(other.gameObject.CompareTag("Projectile") && !enemyState.grabbed && !enemyState.flung)
    	{
    		enemyState.flung = true;
    		hpScript.changeHealth(impactSpeed*-0.75f);
    		weakened.Play();
    	}
    	//Don't take damage when held
    	else if(!enemyState.grabbed)
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
