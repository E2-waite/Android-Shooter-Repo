using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditConverter : MonoBehaviour
{
    public int gems = 0, credits = 0, creditPerGem = 10;
    public float convertTime = 5; 
    float currentTime = 0;
    public EnergyParticles gemParticle, creditParticle;
    private void Start()
    {
        currentTime = convertTime;
    }

    private void Update()
    {
        if (gems > 0)
        {
            if (currentTime <= 0)
            {
                currentTime = convertTime;
                gems--;
                credits += creditPerGem;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
        gemParticle.SetParticle(gems);
        creditParticle.SetParticle(credits / 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            gems += player.gems;
            player.gems = 0;

            player.credits += credits;
            credits = 0;
            UIController.Instance.currency.UpdateText(player.gems, player.maxGems, player.credits);
        }
    }
}
