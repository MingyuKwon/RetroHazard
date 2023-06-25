using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerItemBox : MonoBehaviour
{
    public PlayerInventory inventory;
    public PlayerStatus status;
    BoxItemUI boxItemUI;
    PlayerItemUI playerItemUI;

    public ItemInformation[] items;
    public int[] itemsamount;

    public bool isBoxfull;

    public int SheildBatteryLimit = 8;
    public int Energy1BatteryLimit = 30;
    public int Energy2BatteryLimit = 12;
    public int Energy3BatteryLimit = 9;

    private void Awake() {
        boxItemUI = FindObjectOfType<BoxItemUI>();
        playerItemUI = FindObjectOfType<PlayerItemUI>();
        status = transform.parent.GetComponentInChildren<PlayerStatus>();
        inventory = transform.parent.GetComponentInChildren<PlayerInventory>();
        

        items = new ItemInformation[16];
        itemsamount = new int[16];
    }

    private void OnEnable() {
        GameManager.EventManager.BoxEvent += BoxInOut;
    }

    private void OnDisable() {
        GameManager.EventManager.BoxEvent -= BoxInOut;
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

    private void BoxInOut(bool flag, ItemInformation information, int amount, int currentindex)
    {
        if(flag)
        {
            for(int i=0; i<16; i++)
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

        playerItemUI.UpdateInventoryUI();
        boxItemUI.UpdateBoxItemUI();
    }

    public void removeItem(int index)
    {
        items[index] = null;
        itemsamount[index] = 0;

        playerItemUI.UpdateInventoryUI();
        boxItemUI.UpdateBoxItemUI();
    }

    private void Update() {
        isBoxfull = CheckInventoryFull();
    }

    private bool CheckInventoryFull()
    {
        for(int i=0; i<16; i++)
        {
            if(items[i] == null)
            {
                return false;
            }
            
        }
        return true;
    }
}
