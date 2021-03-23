using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public Text healthText;
    public void UpdateHealthBar(int current, int max)
    {
        fillBar.fillAmount = (float)current / (float)max;
        healthText.text = current.ToString() + "/" + max.ToString();
    }
}
