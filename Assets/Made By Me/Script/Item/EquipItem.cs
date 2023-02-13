using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : KeyItem
{
    public static event Action<ItemInformation, int> Obtain_Equip_Item_Event;

    public override void EventInvokeOverride()
    {
        Obtain_Equip_Item_Event?.Invoke(information, information.KeyItemCode);
        Destroy(this.gameObject);
    }
}
