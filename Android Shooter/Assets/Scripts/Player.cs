using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float maxHealth = 100, currentHealth, speed = 10;
    float moveX, moveZ;
    public Vector3 shootVec;
    public LineRenderer line;
    public GameObject lazerPrefab;
    GameObject lazer;
    public List<GameObject> childLazers = new List<GameObject>();
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        LookAt();
        moveX = Input.GetAxis("Horizontal"); 
        moveZ = Input.GetAxis("Vertical");
        Move();
        if (Input.GetMouseButton(0))
        {
            if (lazer == null)
            {
                lazer = Instantiate(lazerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            lazer.GetComponent<Lazer>().Out(transform.position, transform.forward);
        }
        else
        {
            if (lazer != null)
            {
                // If all children are destroyed - destroy base lazer
                if (lazer.GetComponent<Lazer>().ShutDownChild())
                {
                    Destroy(lazer);
                }
            }
        }
    }

    void Move()
    {
        var dir = new Vector3(moveX * speed, 0, moveZ * speed);
        rb.AddForce(dir);
    }

    void LookAt() // Look at mouse
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        var lookPos = v3 - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }

    public void Hit(float damage) // Take damage when player is hit
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
 