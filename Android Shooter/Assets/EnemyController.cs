using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100, currentHealth, speed = .1f;
    public GameObject player;
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Hit(float damage)
    {
        currentHealth -= damage * Time.deltaTime;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Hit(10);
            Destroy(gameObject);
        }
    }
}
