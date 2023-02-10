using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionItem : KeyItem
{
    public static event Action<ItemInformation, int> Obtain_Expansion_Item_Event;
    public int amount;

    public void ObtainExpansionItem()
    {
        Obtain_Expansion_Item_Event?.Invoke(information , amount);
    }

    public override void EventInvokeOverride()
    {
        ObtainExpansionItem();
        Destroy(this.gameObject);
    }


}
