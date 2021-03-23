using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoSingleton<UIController>
{
    public GameObject ammoUI;
    public HealthBar healthBar;
    public CurrencyUI currency;
    public void SwitchWeaponUI(Player.EquipSlot slot)
    {
        if (slot == Player.EquipSlot.miningLazer)
        {
            ammoUI.SetActive(false);
        }
        if (slot == Player.EquipSlot.gun)
        {
            ammoUI.SetActive(true);
        }
    }
}
