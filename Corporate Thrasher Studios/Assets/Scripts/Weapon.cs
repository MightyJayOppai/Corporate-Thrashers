using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Monobehavior is not needed as it is not going to be attached to anything
//it will simply have the stats of the weapons
[System.Serializable]
public class Weapon 
{
    public int itemID;
    public int weaponType;
    public GameObject weaponObject;
    public string name;
    public float ammo;
    public float damage;
    public float range;
    public float rateOfFire;
}
