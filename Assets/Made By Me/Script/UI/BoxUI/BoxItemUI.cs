using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class BoxItemUI : MonoBehaviour
{
    [SerializeField] Sprite nullSprite;

    public BoxItemContainer[] itemContainers;
    public PlayerItemBox playerItemBox;
    // player Box 
    ItemExplainUI itemExplainUI;

    BoxUI boxUI = null;

    public Image backgroundPanel;
    public Color selectColor = new Color(0.8f,0.8f,0.8f,1);
    public Color unSelectColor = new Color(0.5f,0.5f,0.5f,1);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               

    public int currentindex = 0;
    public bool isBoxFull;

    private void Awake() {
        itemContainers = GetComponentsInChildren<BoxItemContainer>();
        Array.Reverse(itemContainers);

        playerItemBox = FindObjectOfType<PlayerItemBox>();
        itemExplainUI = transform.parent.GetComponentInChildren<ItemExplainUI>();
        backgroundPanel = transform.GetChild(0).GetComponent<Image>();

        boxUI = transform.parent.gameObject.GetComponent<BoxUI>();
    }

    private void Start() {
        UpdateBoxUI();
    }

    private void Update() {
        currentindex = boxUI.boxItemIndex;
        ItemContainerFocus();
        isBoxFull = playerItemBox.isBoxfull;
    }

    private void ItemContainerFocus()
    {
        if(boxUI.currentWindowLayer == 1) return;

        for(int i=0; i < 16; i++)
        {
            if(i == currentindex)
            {
                itemContainers[i].SetFocus(true);
                if(playerItemBox.items[i] != null)
                {
                    itemExplainUI.SetItemExplain(playerItemBox.items[i].ItemDescription[0]);
                    itemExplainUI.SetItemName(playerItemBox.items[i].ItemName);
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
        for(int i=0; i < 16; i++)
        {
            if(i == num)
            {
                itemContainers[i].SetFocus(true);
                if(playerItemBox.items[i] != null)
                {
                    itemExplainUI.SetItemExplain(playerItemBox.items[i].ItemDescription[0]);
                    itemExplainUI.SetItemName(playerItemBox.items[i].ItemName);
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

    

    public void UpdateBoxUI()
    {
        foreach(BoxItemContainer itemContainer in itemContainers)
        {
            itemContainer.gameObject.SetActive(false);
        }
        ShowingBox();
    }

    private void ShowingBox()
    {
        for(int i=0; i<16; i++)
        {
            itemContainers[i].gameObject.SetActive(true);

            if(playerItemBox.items[i] != null)
            {
                itemContainers[i].itemImage.sprite = playerItemBox.items[i].ItemImage;

            }else
            {
                itemContainers[i].itemImage.sprite = nullSprite;
            }

            if(playerItemBox.itemsamount[i] > 1)
            {
                itemContainers[i].SetItemAmountUI(true);
                itemContainers[i].SetItemAmountText(playerItemBox.itemsamount[i].ToString());
            }else
            {
                itemContainers[i].SetItemAmountUI(false);
            }
            
        }
    }
}
