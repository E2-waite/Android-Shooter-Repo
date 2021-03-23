using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoBar : MonoBehaviour
{
    Player player;
    public Image fillBar;
    public Text ammoText, reloadText;
    private void Start()
    {
        player = LevelController.Instance.player;
    }

    private void Update()
    {
        fillBar.fillAmount = (float)player.currGun.currRounds / (float)player.currGun.rounds;
        ammoText.text = player.currGun.currRounds.ToString() + "/" + player.currGun.rounds.ToString();
        if (player.currGun.IsReloading())
        {
            reloadText.enabled = true;
        }
        else
        {
            reloadText.enabled = false;
        }
    }


}
