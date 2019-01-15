using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
	//Time between shots
	public float fireDelay;
	public Rigidbody projectile;
	public float fireForce;

    private float fireTimer;
    private EnemyController parentEntity;
    private Vector3 target;

    void Start()
    {
    	fireTimer = 0.0f;
    	parentEntity = transform.parent.parent.GetComponent<EnemyController>();
    	target = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform.position;
    	//Make it target a bit above the player to account for gravity
    	target = new Vector3(target.x, target.y + 0.5f, target.z);
    }

    void Update()
    {
    	//If the enemy is in a state where they can fire
    	if(!(parentEntity.flung || parentEntity.grabbed) && !parentEntity.dead)
    	{
    		fireTimer += Time.deltaTime;

    		if(fireTimer > fireDelay)
    		{
    			Fire();
    			fireTimer = 0;
    		}
    	}
    	else
    	{
    		fireTimer = 0;
    	}
    }

    void Fire()
    {
    	Rigidbody projectileClone = (Rigidbody) Instantiate(projectile, transform.position, transform.rotation);
    	projectileClone.AddForce((target - projectileClone.transform.position) * fireForce);
    }
}
