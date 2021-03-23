using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    GameObject lazer;
    public bool isLeft;
    public GameObject Reflect(GameObject inLazer, Vector3 inNorm, float inDamage)
    {
        
        if (lazer == null)
        {
            lazer = Instantiate(inLazer, new Vector3(0,0,0), Quaternion.identity);
        }

        Vector3 right = Vector3.Cross(inNorm, Vector3.up);
        var left = -right;
        var forward = Vector3.forward;
        if (isLeft)
        {
            forward = left;
        }
        else
        {
            forward = right;
        }

        lazer.GetComponent<Lazer>().Out(transform.position, forward);
        return lazer;
    }
}
