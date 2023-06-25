using System;
using UnityEditor;
using UnityEngine;

public class PlayerItemBoxLogic 
{
    const int boxSize = 16;
    public ItemInformation[] items;
    public int[] itemsamount;
    public bool isBoxfull = false;

    public PlayerItemBoxLogic()
    {
        items = new ItemInformation[boxSize];
        itemsamount = new int[boxSize];
    }

    public void OnEnable() {
        GameManager.EventManager.BoxEvent += BoxInOut;
    }

    public void OnDisable() {
        GameManager.EventManager.BoxEvent -= BoxInOut;
    }

    public bool CheckItemBoxFull()
    {
        for(int i=0; i<boxSize; i++)
        {
            if(items[i] == null)
            {
                return false;
            }
            
        }
        return true;
    }

    private void BoxInOut(bool flag, ItemInformation information, int amount, int currentindex)
    {
        if(flag)
        {
            for(int i=0; i<boxSize; i++)
            {
                if(items[i] == null)
                {
                    items[i] = information;
                    itemsamount[i] = amount;
                    break;
                }
            }
        }else
        {
            removeItem(currentindex);
        }

        UI.instance.boxUI.playerItemUI.UpdateInventoryUI();
        UI.instance.boxUI.boxItemUI.UpdateBoxItemUI();
    }

    public void removeItem(int index)
    {
        items[index] = null;
        itemsamount[index] = 0;

        UI.instance.boxUI.playerItemUI.UpdateInventoryUI();
        UI.instance.boxUI.boxItemUI.UpdateBoxItemUI();
    }

    public void LoadSave(PlayerBoxItemSave save)
    {
        for(int i=0; i< save.itemsPath.Length; i++)
        {
            items[i] = AssetDatabase.LoadAssetAtPath<ItemInformation>(save.itemsPath[i]);
        }
        Array.Copy( save.itemsamount , itemsamount, save.itemsamount.Length);

        isBoxfull = save.isBoxfull;
    }
}
