using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionItem : KeyItem
{
    public static event Action<bool, bool, bool, int> Obtain_Expansion_Item_Event;
    public bool isEnergy1;
    public bool isEnergy2;
    public bool isEnergy3;

    public int amount;

    public void ObtainExpansionItem()
    {
        Obtain_Expansion_Item_Event?.Invoke(isEnergy1, isEnergy2, isEnergy3, amount);
    }

    public override void EventInvokeOverride()
    {
        ObtainExpansionItem();
        Destroy(this.gameObject);
    }


}
