using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item Class", menuName = "Item/Misc")]

public class MiscClass : ItemClass
{
    public override ItemClass GetItem() { return this; }
    public override ConsumableClass GetConsumable() { return null; }
    public override MiscClass GetMIsc() { return this; }
}