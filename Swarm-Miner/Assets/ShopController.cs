using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoSingleton<ShopController>
{
    public Shop shop;
    CanvasGroup shopCG;
    bool active = false;
    private void Start()
    {
        shopCG = shop.gameObject.GetComponent<CanvasGroup>();
    }
    public void ToggleShop()
    {
        if (active)
        {
            shopCG.alpha = 0;
            LevelController.Instance.SetPause(false);
            active = false;
        }
        else
        {
            shopCG.alpha = 1;
            LevelController.Instance.SetPause(true);
            active = true;
        }
    }
}
