using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class PlayerBoxItemSave 
{
    public String[] itemsPath = new String[16];
    public int[] itemsamount = new int[16];

    public bool isBoxfull;

    public PlayerBoxItemSave()
    {

    }

    public PlayerBoxItemSave(PlayerItemBox itemBox)
    {
        for(int i=0; i< itemBox.items.Length; i++)
        {
            itemsPath[i] = AssetDatabase.GetAssetPath(itemBox.items[i]);
        }
        Array.Copy( itemBox.itemsamount , itemsamount, itemBox.itemsamount.Length);

        isBoxfull = itemBox.isBoxfull;
    }
}
