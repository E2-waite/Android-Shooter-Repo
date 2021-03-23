using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyParticles : MonoBehaviour
{
    ParticleSystem ps;
    Light light;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        light = GetComponent<Light>();
    }

    public void SetParticle(int level)
    {
        var emission = ps.emission;
        emission.rateOverTime = level * 2;
        light.intensity = level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
