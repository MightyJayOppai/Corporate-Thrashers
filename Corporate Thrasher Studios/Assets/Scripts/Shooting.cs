using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour 
{
    public int id;
    float timeToFire;

    [SerializeField]
    int curAmmo;
    float reloadSpeed = 1;

    bool reloading; //= false

	GameObject gameController;
    WeaponDatabase database;
    Camera mainCam;
    Animator weaponAnimator;
    public Transform barrelLocation;
    public GameObject impactEffect;
    public ParticleSystem muzzleFlash;


    void Start()
    {
        if(barrelLocation == null)
        {
            barrelLocation = transform;
        }       

        gameController = GameObject.FindGameObjectWithTag("GameController");
        database = gameController.GetComponent<WeaponDatabase>();
        mainCam = Camera.main;

        id = GetComponent<WeaponID>().weaponID;
        weaponAnimator = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<Animator>();

        curAmmo = database.weapons[id].maxAmmo;
        reloading = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            //To prevent reloading when at max ammo
            if(curAmmo == database.weapons[id].maxAmmo)
            {
                return;
            }
            else
            {
                StartCoroutine(Reload());
                return;
            }
        }
        
        if (curAmmo <= 0)
        {
            // To prevent the player from firing while reloading, it will be going back to Update() and then go to the next frame
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButton("Fire1") && Time.time > timeToFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        curAmmo--;
        
        RaycastHit hit;
        //Going to the gamecontroller, to the database, finding the weapon with the id and getting the range variable from it
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, database.weapons[id].range))
        {
            timeToFire = Time.time + 1f/database.weapons[id].rateOfFire;
            if(hit.transform.tag == "Enemy")
            {
                Debug.Log(hit.transform.name);
                EnemyStats enemystats = hit.transform.GetComponent<EnemyStats>();
                enemystats.TakeDamage(database.weapons[id].damage);
                Debug.Log(database.weapons[id].damage);
                GameObject impactFX =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactFX, 0.5f);

            }
        }
    }

    IEnumerator Reload()
    {
        //Before waiting for a few seconds, the reloading will be true when the player is waiting
        reloading = true;
        weaponAnimator.SetBool ("Reloading", true);
        
        yield return new WaitForSeconds (reloadSpeed);
        curAmmo = database.weapons[id].maxAmmo;
        reloading = false;
        weaponAnimator.SetBool ("Reloading", false);
    }
}
