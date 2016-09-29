using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name { get; set; }
    public IEnumerable<Rarity> Rarity { get; set; }
}

public enum Rarity
    {
        Broken,
        Common,
        Rare,
        Legendary
    }

