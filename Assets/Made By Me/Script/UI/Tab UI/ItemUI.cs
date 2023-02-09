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
    

    private void Awake() {
        itemContainers = GetComponentsInChildren<ItemContainer>();
        playerInventory = FindObjectOfType<PlayerInventory>();
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
