using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemType
    {
        weapon,
        explosive,
        support,
        placeable,
        upgrade,
        companion
    }
    public string name;
    [TextArea] public string description;
    public ItemType type;
    public int cost;
    public Sprite icon;
}
