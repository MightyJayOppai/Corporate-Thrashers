using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetTest : MonoBehaviour {

	public float health;
	public float maxHealth;
	public GameObject healthBarUI;
	public Slider slider;

	public void TakeDamage(float amt)
	{
		health -= amt;
		if(health <= 0f)
		{
			Killed();
		}
	}

	void Killed ()
	{
		Destroy (gameObject);
	}
	void Start () 
	{
		health = maxHealth;
		slider.value = CalculateHealth();
	}
	
	
	void Update () 
	{
		slider.value = CalculateHealth();

		if(health < maxHealth)
		{
			healthBarUI.SetActive(true);
		}

		if(health > maxHealth)
		{
			health = maxHealth;
		}
	}
	float CalculateHealth()
	{
		return health/maxHealth;
	}
}
