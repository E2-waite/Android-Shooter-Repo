using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<Gun> weapons = new List<Gun>();
    public List<Explosive> explosives = new List<Explosive>();
    public List<Support> supports = new List<Support>();
    public List<Placeable> placeables = new List<Placeable>();
    public int gunInd = 0;

    public void AddItem(Item item)
    {
        if (item.type == Item.ItemType.weapon)
        {
            weapons.Add((Gun)item);
        }
        else if (item.type == Item.ItemType.explosive)
        {
            explosives.Add((Explosive)item);
        }
        else if (item.type == Item.ItemType.support)
        {
            supports.Add((Support)item);
        }
        else if (item.type == Item.ItemType.placeable)
        {
            placeables.Add((Placeable)item);
        }
    }
}
