using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTurret : MonoBehaviour
{
    public bool firing = false;
    public float fireTime = 5, cooldown = 5;
    float fireTimer, cooldownTimer;
    
    public GameObject lazerPrefab;
    GameObject lazer;
    void Start()
    {
        StartCoroutine(Fire()); 
    }

    IEnumerator Fire()
    {
        lazer = Instantiate(lazerPrefab, new Vector3(0,0,0), Quaternion.identity);

        firing = true;
        fireTimer = fireTime;
        while (fireTimer > 0)
        {
            lazer.GetComponent<Lazer>().Out(transform.position, transform.forward);
            fireTimer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        if (lazer.GetComponent<Lazer>().ShutDownChild())
        {
            Destroy(lazer);
        }

        firing = false;
        cooldownTimer = cooldown;
        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Fire());
    }
}
