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

    public void ObtainKeyItem(Collider2D other)
    {
        base.Get_Item_Pause_Game();

        GetComponentInChildren<Interact>().check.SetActive(false);
        transform.position = other.gameObject.transform.position + base.itemUp;
        Obtain_Expansion_Item_Event?.Invoke(isEnergy1, isEnergy2, isEnergy3, amount);
    }
}
