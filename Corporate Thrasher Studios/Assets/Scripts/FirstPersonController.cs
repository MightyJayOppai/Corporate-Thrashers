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
    public float stamina;
    public float maxStamina;
    public GameObject staminaBarUI;
	public Slider staminaSlider;

    private Rigidbody myRB;
    private Vector3 movementInput;
    private float moveXAxis;
    private float moveYAxis;

   
    void Start ()
    {  
        myRB = GetComponent<Rigidbody>();
        playerSpeed = playerWalkSpeed;
        stamina = maxStamina;
        staminaSlider.value = CalculateStamina();

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

		if(stamina < maxStamina)
		{
			staminaBarUI.SetActive(true);
		}

		if(stamina > maxStamina)
		{
			stamina = maxStamina;
		}
        
        /*if(movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation, Quaternion.LookRotation(movementInput),0.15f);
            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce(movementInput * playerSpeed, ForceMode.Impulse);
        }*/

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina -= Time.deltaTime;
            playerSpeed = playerRunSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && stamina < maxStamina || stamina == 0)
        {
            playerSpeed = playerWalkSpeed;
            stamina += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && myRB.velocity.y == 0)
        {
            myRB.AddForce(new Vector3(0,10,0), ForceMode.Impulse);
        }


    }
    float CalculateStamina()
    {
        return stamina/maxStamina;
    }
}
