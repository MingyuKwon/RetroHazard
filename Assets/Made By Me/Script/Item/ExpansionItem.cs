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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            base.Get_Item_Pause_Game();

            transform.position = other.gameObject.transform.position + base.itemUp;
            Obtain_Expansion_Item_Event?.Invoke(isEnergy1, isEnergy2, isEnergy3, amount);
        }
    }
}
