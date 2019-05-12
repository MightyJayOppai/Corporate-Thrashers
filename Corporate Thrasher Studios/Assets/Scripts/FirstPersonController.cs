using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour 
{
	public float playerSpeed;
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float clampMax;
    public float curHealth;
    public float maxHealth;
    public float curStamina;
    public float maxStamina;
    public GameObject healthBarUI;
    public GameObject staminaBarUI;
	public Slider healthSlider;
    public Slider staminaSlider;

    private Rigidbody myRB;
    private Vector3 movementInput;
    private float moveXAxis;
    private float moveYAxis;

    bool isDead = false;

   
    void Start ()
    {  
        myRB = GetComponent<Rigidbody>();
        playerSpeed = playerWalkSpeed;
        curHealth = maxHealth;
        curStamina = maxStamina;
        staminaSlider.value = CalculateStamina();
        healthSlider.value = CalculateHealth();

         //to lock the mouse to the game
        Cursor.lockState = CursorLockMode.Locked;
    }

	void FixedUpdate ()
    {

        moveXAxis = Input.GetAxis("Horizontal");
        moveYAxis = Input.GetAxis("Vertical");

        movementInput = (moveXAxis * transform.right + moveYAxis * transform.forward).normalized;
        //movementInput = new Vector3(moveXAxis, 0.0f, MoveYAxis);   
        if(movementInput != Vector3.zero)
        {
            myRB.AddForce(movementInput * playerSpeed, ForceMode.Impulse);
            //myRB.MovePosition(transform.position + transform.forward * playerSpeed);
        }

        staminaSlider.value = CalculateStamina();
        healthSlider.value = CalculateHealth();

		if(curStamina < maxStamina)
		{
			staminaBarUI.SetActive(true);
		}

		if(curStamina > maxStamina)
		{
			curStamina = maxStamina;
		}

        if(curHealth < maxHealth)
		{
			healthBarUI.SetActive(true);
		}

		if(curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
        
        /*if(movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation, Quaternion.LookRotation(movementInput),0.15f);
            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce(movementInput * playerSpeed, ForceMode.Impulse);
        }*/

        if (Input.GetKey(KeyCode.LeftShift) && curStamina > 0)
        {
            curStamina -= Time.deltaTime;
            playerSpeed = playerRunSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && curStamina < maxStamina || curStamina == 0)
        {
            playerSpeed = playerWalkSpeed;
            curStamina += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && myRB.velocity.y == 0)
        {
            myRB.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
        }

    }
    float CalculateStamina()
    {
        return curStamina/maxStamina;
    }
    float CalculateHealth()
	{
		return curHealth/maxHealth;
	}

    public void CheckHealth()
    {
        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            curHealth = 0;
            isDead = true;
        }
    }

    public void CheckStamina()
    {
        if (curStamina >= maxStamina)
        {
            curStamina = maxStamina;
        }

        if (curStamina <= 0)
        {
            curStamina = 0;

        }
    }
    public void Die()
    {
        Debug.Log("You have Died!");
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
}
