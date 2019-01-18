using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public float maxHealth;
    public Image healthImage;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void UpdateSize()
    {
    	healthImage.fillAmount = currentHealth/maxHealth;
    }

    public void changeHealth(float change)
    {
    	currentHealth += change;
    	if(currentHealth < 0)
    	{
    		currentHealth = 0;

    	}
    	else if(currentHealth > maxHealth)
    	{
    		currentHealth = maxHealth;
    	}
    	UpdateSize();
    }

    public float getHealth()
    {
    	return currentHealth;
    }
}
