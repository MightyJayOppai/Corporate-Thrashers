using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider healthSlider;
    bool isDead = false;
    void Start()
    {
        curHealth = maxHealth;
        //healthSlider.value = CalculateHealth();

    }
    void FixedUpdate()
    {
        healthSlider.value = CalculateHealth();

        if(curHealth < maxHealth)
		{
			healthBarUI.SetActive(true);
		}

		if(curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
    }
    float CalculateHealth()
	{
		return curHealth/maxHealth;
	}
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
}

