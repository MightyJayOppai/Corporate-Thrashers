using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    //private CharacterController myCC;
    private Rigidbody multiRB;
    public float playerSpeed;
    public float rotation;
    private Vector3 movementInput;
    private float moveXAxis;
    private float moveYAxis;
    
    void Start()
    {
        multiRB = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        //myCC = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        if(PV.IsMine)
        {
            BasicMovement();
            BasicRotation();
        }
    }
    void BasicMovement()
    {
        moveXAxis = Input.GetAxis("Horizontal");
        moveYAxis = Input.GetAxis("Vertical");

        movementInput = (moveXAxis * transform.right + moveYAxis * transform.forward).normalized;
        //movementInput = new Vector3(moveXAxis, 0.0f, MoveYAxis);   
        if(movementInput != Vector3.zero)
        {
            multiRB.AddForce(movementInput * playerSpeed, ForceMode.Impulse);
            //myRB.MovePosition(transform.position + transform.forward * playerSpeed);
        }
    }
    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotation;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
