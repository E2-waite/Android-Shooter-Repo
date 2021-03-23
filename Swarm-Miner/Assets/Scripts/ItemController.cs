using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoSingleton<ItemController>
{
    public List<Gun> guns = new List<Gun>();
    public List<Explosive> explosives = new List<Explosive>();
    public List<Support> supportItems = new List<Support>();
    public List<Placeable> placeables = new List<Placeable>();
}
