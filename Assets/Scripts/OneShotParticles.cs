using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticles : MonoBehaviour
{

    public ParticleSystem _ParticleSystem;
	
    // Update is called once per frame
    void Update()
    {
        if (!_ParticleSystem.IsAlive()) {
            Destroy(gameObject);
        }
    }
}
