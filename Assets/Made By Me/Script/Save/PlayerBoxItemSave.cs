using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBoxItemSave 
{
    public ItemInformation[] items = new ItemInformation[16];
    public int[] itemsamount = new int[16];

    public bool isBoxfull;

    public PlayerBoxItemSave()
    {

    }

    public PlayerBoxItemSave(PlayerItemBox itemBox)
    {
        Array.Copy( itemBox.items , items, itemBox.items.Length);
        Array.Copy( itemBox.itemsamount , itemsamount, itemBox.itemsamount.Length);

        isBoxfull = itemBox.isBoxfull;
    }
}
