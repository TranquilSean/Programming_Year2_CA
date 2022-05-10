using System.Collections;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;

    public abstract ItemClass GetItem();
    public abstract ConsumableClass GetConsumable();
    public abstract MiscClass GetMIsc();
}
