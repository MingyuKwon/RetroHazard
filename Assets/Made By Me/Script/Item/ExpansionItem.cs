using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionItem : KeyItem
{
    public static event Action<ItemInformation, int> Obtain_Expansion_Item_Event;

    public override void EventInvokeOverride()
    {
        Obtain_Expansion_Item_Event?.Invoke(information , information.expansionAmount);
        Destroy(this.gameObject);
    }
}
