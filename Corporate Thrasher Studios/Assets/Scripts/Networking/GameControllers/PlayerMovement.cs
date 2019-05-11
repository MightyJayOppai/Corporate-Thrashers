using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController myCC;
    public float movementSpeed;
    public float rotation;
    
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
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
        if(Input.GetKey(KeyCode.W))
        {
            myCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        }

        if(Input.GetKey(KeyCode.S))
        {
            myCC.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            myCC.Move(transform.right * Time.deltaTime * movementSpeed);
        }

        if(Input.GetKey(KeyCode.A))
        {
            myCC.Move(-transform.right * Time.deltaTime * movementSpeed);
        }
    }
    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotation;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
