using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item Class", menuName = "Item/Consumable")]

public class ConsumableClass : ItemClass
{
    public float healthPoints;
    public override ItemClass GetItem() { return this; }
    public override ConsumableClass GetConsumable() { return this; }
    public override MiscClass GetMIsc() { return null; }
}