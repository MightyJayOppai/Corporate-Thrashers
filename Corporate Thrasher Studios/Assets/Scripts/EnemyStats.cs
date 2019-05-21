using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float damage;
    public float curHealth;
    public float maxHealth;
    public float maxSpeed;
    public float maxForce;
    [SerializeField]
    float stoppingDis;
    private GameObject player;
    private Rigidbody enemyRB;
    public GameObject healthBarUI;
    public Slider healthSlider;
    bool isDead = false;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        curHealth = maxHealth;
        //healthSlider.value = CalculateHealth();

    }
    void FixedUpdate()
    {
        Vector3 desiredVelocity = (player.transform.position - transform.position).normalized * maxSpeed;
        Vector3 steering  = desiredVelocity - enemyRB.velocity;
        Vector3 clampSteering = Vector3.ClampMagnitude(steering, maxForce);
        enemyRB.AddForce(clampSteering);

        transform.LookAt(transform.position + enemyRB.velocity);

        float dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis < stoppingDis)
        {
            player.GetComponent<FirstPersonController>().TakeDamage(damage);
            player.GetComponent<FirstPersonController>().CheckHealth();
        }

        // Depending on the time passed in game subtracted with the last attack time, if greater than the cooldown

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

