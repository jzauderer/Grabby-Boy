using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStanding : MonoBehaviour
{
    private Transform torso;
    private Transform ArmR;
    private Transform ArmL;
    private Transform LegR;
    private Transform LegL;

    void Start()
    {
    	torso = transform.GetChild(1);
    	ArmR = transform.GetChild(2);
    	ArmL = transform.GetChild(3);
    	LegR = transform.GetChild(4);
    	LegL = transform.GetChild(5);
    }

    void FixedUpdate()
    {
        
    }
}
