using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        health,
        gem
    }
    public PickupType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((type == PickupType.health && other.GetComponent<Player>().Heal()) || 
                (type == PickupType.gem && other.GetComponent<Player>().PickupGem()))
            {
                Destroy(gameObject);
            }
        }
    }
}
