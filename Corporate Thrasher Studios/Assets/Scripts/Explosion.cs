using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem bulletImpact;
    
    void Start()
    {
        bulletImpact = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Explosion") 
        {
            Destroy (gameObject);
        }
    }     

    void Update()
    {
        
    }
}
