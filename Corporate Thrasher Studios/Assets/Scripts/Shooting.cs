using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float damage;
	public float range;
	public float fireRate;
	public float impactForce;
	public float firePower;
	public GameObject casingPrefab;
	//To instantiate into the scene
	public GameObject impactEffect;
	public Transform barrelLocation;
    public Transform casingExitLocation;
	public ParticleSystem muzzleFlash;
	public Camera fpsCam;

	private float timeToFire = 0f;

	void Start () 
	{
		if(barrelLocation == null)
		{
			barrelLocation = transform;
		}
	}
	
	
	void Update () 
	{
		if(Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
		{
			timeToFire = Time.time + 1f/fireRate;
			GetComponent<Animator>().SetTrigger("Fire");
		}
	}

	void Shoot ()
	{
		muzzleFlash.Play();
		
		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			//Debug.Log (hit.transform.name);
			TargetTest target = hit.transform.GetComponent<TargetTest>();
			if(target != null)
			{
				target.TakeDamage(damage);
			}

			if(hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * impactForce);
			}
			//Instantiate does not take a direction to look, it takes a quaternion
			//LookRotation to take a direction and turn it to quaternion (hit.normal)
			GameObject impactFX =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactFX, 0.5f);
		}
	}

	void CasingRelease ()
	{
		GameObject handguncasing;
		handguncasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
		handguncasing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        handguncasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse); 
		Destroy(handguncasing, 0.75f);
	}
}
