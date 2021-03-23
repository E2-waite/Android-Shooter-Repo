using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public Text nameTxt, descriptionTxt;
    public ShopItem[] shopItems = new ShopItem[9];

    public void SetCurrentItem(Item item)
    {
        nameTxt.text = item.name;
        descriptionTxt.text = item.description;
    }

    public void ClearCurrentItem()
    {
        nameTxt.text = "";
        descriptionTxt.text = "";
    }
}
