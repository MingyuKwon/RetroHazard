using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerItemUI : MonoBehaviour
{
    [SerializeField] Sprite nullSprite;

    public PlayerItemContainer[] itemContainers;
    public PlayerInventory playerInventory;
    ItemExplainUI itemExplainUI;

    BoxUI boxUI = null;

    public Image backgroundPanel;
 
    public Color selectColor = new Color(0.8f,0.8f,0.8f,1);
    public Color unSelectColor = new Color(0.5f,0.5f,0.5f,1);

    public int currentindex = 0;
    public bool isInventoryFull;

    private void Awake() {
        itemContainers = GetComponentsInChildren<PlayerItemContainer>();
        Array.Reverse(itemContainers);

        playerInventory = FindObjectOfType<PlayerInventory>();
        itemExplainUI = transform.parent.GetComponentInChildren<ItemExplainUI>();
        backgroundPanel = transform.GetChild(0).GetComponent<Image>();

        boxUI = transform.parent.gameObject.GetComponent<BoxUI>();
    }

    private void Start() {
        UpdateInventoryUI();
    }

    private void OnEnable() {
        UpdateInventoryUI();
    }

    private void Update() {
        currentindex = boxUI.playerItemIndex;
        ItemContainerFocus();
        isInventoryFull = playerInventory.isInventoryFull;
    }

    public void UpdateInventoryUI()
    {
        foreach(PlayerItemContainer itemContainer in itemContainers)
        {
            itemContainer.gameObject.SetActive(false);
        }
        ShowingInventory();
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

    private void ItemContainerFocus()
    {
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
}
