using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveShield : MonoBehaviour
{
    Renderer rend;

    public float currHealth, maxHealth = 1000;
   

    void Start()
    {
        rend = GetComponent<Renderer>();
        currHealth = maxHealth;
        rend.sharedMaterial.SetFloat("opacity", currHealth / maxHealth);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float val = 0, end = 1;

        while (val < end)
        {
            val += Time.deltaTime;
            rend.sharedMaterial.SetFloat("pulse", val);
            yield return null;
        }
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float val = 1, end = 0;

        while (val > end)
        {
            val -= Time.deltaTime;
            rend.sharedMaterial.SetFloat("pulse", val);
            yield return null;
        }
        StartCoroutine(FadeIn());
    }

    public void Hit(float damage)
    {
        currHealth -= damage;
        rend.sharedMaterial.SetFloat("opacity", currHealth / maxHealth);
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
