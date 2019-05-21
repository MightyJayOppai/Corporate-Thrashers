using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    public float playerSpeed;
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float walkClampMax;
    public float runClampMax;
    public float curHealth;
    public float maxHealth;
    public float curStamina;
    public float maxStamina;
    public GameObject healthBarUI;
    public GameObject staminaBarUI;
    public Slider healthSlider;
    public Slider staminaSlider;
    [Header("Mobile Joystick")]
    [SerializeField]
    private MobileJoystick mjMobileJoy;
    private GameObject mjMobilePref;
    private Rigidbody myRB;
    private Vector3 movementInput;
    private float moveXAxis;
    private float moveYAxis;

    bool isDead = false;


    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        playerSpeed = playerWalkSpeed;
        curHealth = maxHealth;
        curStamina = maxStamina;
        mjMobileJoy = GameObject.FindGameObjectWithTag("MoveJoystick").GetComponent<MobileJoystick>();
        mjMobilePref = GameObject.FindGameObjectWithTag("MoveJoystick");
        staminaSlider.value = CalculateStamina();
        healthSlider.value = CalculateHealth();

        //to lock the mouse to the game
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

#if UNITY_EDITOR || UNITY_STANDALONE
        moveXAxis = Input.GetAxis("Horizontal");
        moveYAxis = Input.GetAxis("Vertical");
        mjMobilePref.SetActive(false);

#else
        float moveXAxis = mjMobileJoy.Horizontal();
        float moveYAxis = mjMobileJoy.Vertical();
        mjMobilePref.SetActive(true);
#endif
        movementInput = (moveXAxis * transform.right + moveYAxis * transform.forward).normalized;

        //movementInput = new Vector3(moveXAxis, 0.0f, moveYAxis);
        if (movementInput != Vector3.zero)
        {
            movementInput = Vector3.ClampMagnitude(movementInput, walkClampMax);
            myRB.AddForce(movementInput * playerSpeed * Time.deltaTime, ForceMode.Impulse);
            //Debug.Log(myRB.velocity.magnitude);

            //myRB.MovePosition(transform.position + transform.forward * playerSpeed);
        }

        staminaSlider.value = CalculateStamina();
        healthSlider.value = CalculateHealth();

        if (curStamina < maxStamina)
        {
            staminaBarUI.SetActive(true);
        }

        if (curStamina > maxStamina)
        {
            curStamina = maxStamina;
        }

        if (curHealth < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        /*if(movementInput != Vector3.zero)
        {
            myRB.rotation = Quaternion.Slerp(myRB.rotation, Quaternion.LookRotation(movementInput),0.15f);
            movementInput = Vector3.ClampMagnitude(movementInput, clampMax);
            myRB.AddForce(movementInput * playerSpeed, ForceMode.Impulse);
        }*/

        if (Input.GetKey(KeyCode.LeftShift) && curStamina >= 0)
        {
            curStamina -= Time.deltaTime;
            // playerSpeed = playerRunSpeed;
            movementInput = Vector3.ClampMagnitude(movementInput, runClampMax);
            myRB.AddForce(movementInput * playerRunSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && curStamina <= maxStamina)
        {
            playerSpeed = playerWalkSpeed;
            curStamina += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && myRB.velocity.y == 0)
        {
            myRB.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        }

    }
    float CalculateStamina()
    {
        return curStamina / maxStamina;
    }
    float CalculateHealth()
    {
        return curHealth / maxHealth;
    }

    public void CheckHealth()
    {
        if (healthSlider.value >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (healthSlider.value <= 0)
        {
            curHealth = 0;
            Die();
        }
    }

    public void CheckStamina()
    {
        if (staminaSlider.value >= maxStamina)
        {
            curStamina = maxStamina;
        }

        if (staminaSlider.value <= 0)
        {
            curStamina = 0;

        }
    }
    public void Die()
    {
        isDead = true;
        Debug.Log("You have Died!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);       
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
}
