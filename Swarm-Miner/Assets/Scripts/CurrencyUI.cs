using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrencyUI : MonoBehaviour
{
    public Text gemsText, creditsText;

    public void UpdateText(int currGems, int maxGems, int credits)
    {
        gemsText.text = currGems.ToString() + "/" + maxGems.ToString();
        creditsText.text = credits.ToString();
    }
}
