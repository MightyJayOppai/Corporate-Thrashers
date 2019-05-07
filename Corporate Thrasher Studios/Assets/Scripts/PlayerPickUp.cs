using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : MonoBehaviour
{
    float pickUpRange = 2f;
    int pickUpLayerMask;
    Camera cam;
    [SerializeField]
    Image pickUpPopUp;
    [SerializeField]
    Text weaponName;   
    [SerializeField]
    GameObject mainWeapon;
    GameObject gameController;
    WeaponDatabase database;
    PlayerInventory inventory;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag ("GameController");
        database = gameController.GetComponent<WeaponDatabase>();
        inventory = gameController.GetComponent<PlayerInventory>();
        cam  = GetComponent<Camera>();

        pickUpLayerMask = LayerMask.GetMask("PickUp");
        DisablePickUpPopUp();
    }

    void Update()
    {
        //Get a point in the screen and sends a ray from that point to the game world
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        //Every frame send a ray cast from the point of screen to the point of world
        if(Physics.Raycast(ray, out hit, pickUpRange, pickUpLayerMask))
        {
            int id = hit.transform.GetComponent<WeaponID>().weaponID;
            EnablePickUpPopUp();
            weaponName.text = database.weapons[id].name;
            if(Input.GetKeyDown(KeyCode.E))
            {
                
                if(database.weapons[id].weaponType == 1)
                {
                    //If the player is not holding any weapon, i.e., id = 0
                    if(inventory.inventory[0] == id)
                    {
                        Debug.Log("You already have that weapon");
                    }
                    else if (inventory.inventory[0] == id)
                    {    
                        inventory.inventory[0] = id;
                        mainWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[0].gameObject.transform.position, inventory.weaponSlot[0].gameObject.transform.rotation);
                        mainWeapon.transform.SetParent(inventory.weaponSlot[0].transform);
                    }
                    else if (inventory.inventory[0] != id)
                    {    
                        Destroy(mainWeapon.gameObject);
                        
                        inventory.inventory[0] = id;
                        mainWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[0].gameObject.transform.position, inventory.weaponSlot[0].gameObject.transform.rotation);
                        mainWeapon.transform.SetParent(inventory.weaponSlot[0].transform);
                    }
                }
            }
        }
        else
        {
            DisablePickUpPopUp();
        }
    }

    void EnablePickUpPopUp()
    {
        pickUpPopUp.gameObject.SetActive(true);
    }
    void DisablePickUpPopUp()
    {
        pickUpPopUp.gameObject.SetActive(false);
    }
}
