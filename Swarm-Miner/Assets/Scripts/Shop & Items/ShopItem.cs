using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
public class ShopItem : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    Shop shop;
    public GameObject buyButton, discardButton;
    public Text refreshText;
    public Image image;
    public Item item;
    Player player;
    public float timer, refreshTime = 5;
    bool refreshing = false;
    private void Start()
    {
        player = LevelController.Instance.player;
        shop = transform.parent.GetComponent<Shop>();
        image = GetComponent<Image>();
        SetupItem();
    }

    void SetupItem()
    {
        Item.ItemType type = (Item.ItemType)Random.Range(0, 3);
        if (type == Item.ItemType.weapon)
        {
            item = ItemController.Instance.guns[Random.Range(0, ItemController.Instance.guns.Count - 1)];
        }
        if (type == Item.ItemType.explosive)
        {
            item = ItemController.Instance.explosives[Random.Range(0, ItemController.Instance.explosives.Count - 1)];
        }
        if (type == Item.ItemType.support)
        {
            item = ItemController.Instance.supportItems[Random.Range(0, ItemController.Instance.supportItems.Count - 1)];
        }
        if (type == Item.ItemType.placeable)
        {
            item = ItemController.Instance.placeables[Random.Range(0, ItemController.Instance.placeables.Count - 1)];
        }
        image.sprite = item.icon;
        image.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!refreshing)
        {
            if (eventData.pointerCurrentRaycast.gameObject == buyButton)
            {
                Buy();
            }
            if (eventData.pointerCurrentRaycast.gameObject == discardButton)
            {
                StartCoroutine(RefreshItem());
            }
        }
    }

    void Buy()
    {
        if (player.credits >= item.cost)
        {
            player.inventory.AddItem(item);
            player.credits -= item.cost;
            StartCoroutine(RefreshItem());
        }
    }

    IEnumerator RefreshItem()
    {
        refreshText.gameObject.SetActive(true);
        timer = refreshTime;
        shop.ClearCurrentItem();
        refreshing = true;
        buyButton.SetActive(false);
        discardButton.SetActive(false);
        image.sprite = null;
        image.color = Color.black;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            refreshText.text = ((int)timer).ToString();
            yield return null;
        }
        refreshText.gameObject.SetActive(false);
        SetupItem();
        refreshing = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!refreshing)
        {
            buyButton.SetActive(true);
            discardButton.SetActive(true);
            shop.SetCurrentItem(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buyButton.SetActive(false);
        discardButton.SetActive(false);
        shop.ClearCurrentItem();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
    }


}
