using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class AvatarCombat : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetUp avatarSetup;
    public Transform rayOrigin;
    public Text healthDisplay;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetUp>();
        healthDisplay = GameSetUp.GS.health;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }
        if(Input.GetButton("Fire1"))
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
        healthDisplay.text = avatarSetup.playerHealth.ToString();
        
    }

    [PunRPC]
    void RPC_Shooting()
    {
        //Raycast is essentially a hidden line and it can be specified as to where it can start as well as the direction it can travel
            RaycastHit hit;
            
            if(Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 1000))
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Something was HIT");

                if(hit.transform.tag == "Avatar")
                {
                    hit.transform.gameObject.GetComponent<AvatarSetUp>().playerHealth -= avatarSetup.playerDamage;
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Nothing was HIT");
            }
    }
}
