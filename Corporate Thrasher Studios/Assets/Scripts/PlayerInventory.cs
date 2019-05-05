using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int[] inventory;
    public GameObject[] weaponSlot;
    void Start()
    {
        inventory = new int[1];
        weaponSlot = new GameObject [1];

        weaponSlot [0] = GameObject.FindGameObjectWithTag ("MainWeapon");
    }

    
    void Update()
    {
        
    }
}
