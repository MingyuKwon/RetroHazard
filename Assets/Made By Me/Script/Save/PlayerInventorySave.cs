using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventorySave
{
    public ItemInformation[] items = new ItemInformation[16];
    public int[] itemsamount = new int[16];
    public bool[] isEquipped = new bool[16];


    public int CurrentContainer;

    public bool isInventoryFull;

    public int SheildBatteryLimit = 8;
    public int Energy1BatteryLimit = 30;
    public int Energy2BatteryLimit = 12;
    public int Energy3BatteryLimit = 9;

    public PlayerInventorySave(PlayerInventory inventory)
    {
        Array.Copy( inventory.items , items, inventory.items.Length);
        Array.Copy( inventory.itemsamount , itemsamount, inventory.itemsamount.Length);
        Array.Copy( inventory.isEquipped , isEquipped, inventory.isEquipped.Length);

        CurrentContainer = inventory.CurrentContainer;

        isInventoryFull = inventory.isInventoryFull;
    }

    public PlayerInventorySave()
    {

    }
}
