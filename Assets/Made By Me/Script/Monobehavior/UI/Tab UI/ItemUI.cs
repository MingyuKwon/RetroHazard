using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
public class ItemUI : MonoBehaviour
{
    [SerializeField] Sprite nullSprite;

    public ItemContainer[] itemContainers;
    public PlayerInventory playerInventory;
    ItemExplainUI itemExplainUI;

    TabUI tabUI = null;

    public int currentindex = 0;
    public bool isInventoryFull;

    private void Awake() {
        itemContainers = GetComponentsInChildren<ItemContainer>();
        Array.Reverse(itemContainers);

        playerInventory = FindObjectOfType<PlayerInventory>();
        itemExplainUI = transform.parent.GetComponentInChildren<ItemExplainUI>();

        tabUI = transform.parent.gameObject.GetComponent<TabUI>();
    }

    public void UpdateInventoryUI()
    {
        foreach(ItemContainer itemContainer in itemContainers)
        {
            itemContainer.gameObject.SetActive(false);
        }
        ShowingInventory();
    }

    private void Start() {
        UpdateInventoryUI();
    }

    private void Update() {

        currentindex = tabUI.currentItemindex;
        ItemContainerFocus();
        isInventoryFull = playerInventory.isInventoryFull;
    }

    private void ItemContainerFocus()
    {
        if(tabUI.currentWindowLayer == 1) return;

        for(int i=0; i < playerInventory.CurrentContainer; i++)
        {
            if(i == currentindex)
            {
                itemContainers[i].SetFocus(true);
                if(playerInventory.items[i] != null)
                {
                    itemExplainUI.SetItemExplain(playerInventory.items[i].ItemDescription[0]);
                    itemExplainUI.SetItemName(playerInventory.items[i].ItemName);
                }else
                {
                    itemExplainUI.SetItemExplain(" ");
                    itemExplainUI.SetItemName(" ");
                }
                
            }else
            {
                itemContainers[i].SetFocus(false);
            }
            
        }
    }

    public void ItemContainerFocusDirect(int num)
    {
        for(int i=0; i < playerInventory.CurrentContainer; i++)
        {
            if(i == num)
            {
                itemContainers[i].SetFocus(true);
                if(playerInventory.items[i] != null)
                {
                    itemExplainUI.SetItemExplain(playerInventory.items[i].ItemDescription[0]);
                    itemExplainUI.SetItemName(playerInventory.items[i].ItemName);
                }else
                {
                    itemExplainUI.SetItemExplain(" ");
                    itemExplainUI.SetItemName(" ");
                }
                
            }else
            {
                itemContainers[i].SetFocus(false);
            }
            
        }
    }

    public void SetInteractFade()
    {
        foreach(ItemContainer itemContainer in itemContainers)
        {
            itemContainer.SetInteractFade();
        }
    }

    

    private void ShowingInventory()
    {
        for(int i=0; i<playerInventory.CurrentContainer; i++)
        {
            itemContainers[i].gameObject.SetActive(true);

            if(playerInventory.isEquipped[i])
            {
                itemContainers[i].EquipImage.gameObject.SetActive(true);
            }else
            {
                itemContainers[i].EquipImage.gameObject.SetActive(false);
            }


            if(playerInventory.items[i] != null)
            {
                itemContainers[i].itemImage.sprite = playerInventory.items[i].ItemImage;

            }else
            {
                itemContainers[i].itemImage.sprite = nullSprite;
            }

            if(playerInventory.itemsamount[i] > 1)
            {
                itemContainers[i].SetItemAmountUI(true);
                itemContainers[i].SetItemAmountText(playerInventory.itemsamount[i].ToString());
            }else
            {
                itemContainers[i].SetItemAmountUI(false);
            }
            
        }
    }
}
