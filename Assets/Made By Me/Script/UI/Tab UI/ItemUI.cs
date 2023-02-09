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
    ItemContainer[] itemContainers;
    PlayerInventory playerInventory;
    ItemExplainUI itemExplainUI;

    TabUI tabUI;
    public int currentindex = 0;
    

    private void Awake() {
        itemContainers = GetComponentsInChildren<ItemContainer>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        itemExplainUI = FindObjectOfType<ItemExplainUI>();
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
    }

    private void ItemContainerFocus()
    {
        for(int i=0; i < playerInventory.CurrentContainer; i++)
        {
            if(i == currentindex)
            {
                itemContainers[i].focus.SetFocus(true);
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
                itemContainers[i].focus.SetFocus(false);
            }
            
        }
    }

    

    private void ShowingInventory()
    {
        for(int i=0; i<playerInventory.CurrentContainer; i++)
        {
            itemContainers[i].gameObject.SetActive(true);
            if(playerInventory.items[i] != null)
            {
                itemContainers[i].itemImage.sprite = playerInventory.items[i].ItemImage;
            }else
            {
                itemContainers[i].itemImage.sprite = nullSprite;
            }
            
        }
    }
}
