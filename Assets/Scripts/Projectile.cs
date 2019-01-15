using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float lifespan;

	//Projectile does not start active, so it doesnt collide with the enemy as its thrown
	public float activeStart;

	private float lifeTimer;
	private float tangibleTimer;
	private Collider myCollider;

    void Start()
    {
        lifeTimer = 0.0f;
        tangibleTimer = 0.0f;
        myCollider = GetComponent<Collider>();
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        tangibleTimer += Time.deltaTime;

        //Set the collider active once its left the grip of the enemy
        if(tangibleTimer > activeStart)
        {
        	myCollider.enabled = true;
        }

        if(lifeTimer > lifespan)
        {
        	Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
    	if(other.gameObject.transform.parent != null)
    	{
    		if(other.gameObject.transform.parent.tag == "Enemy")
	    	{
	    		other.gameObject.transform.parent.GetComponent<EnemyController>().flung = true;
	    		//Destroy(gameObject);
	    	}
    	}
    }
}
