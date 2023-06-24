using UnityEngine;
using System;


public class ItemUILogic 
{
    Sprite nullSprite;
    public ItemContainer[] itemContainers;

    public ItemUILogic(Sprite nullSprite , ItemContainer[] _itemContainers)
    {
        this.nullSprite = nullSprite;
        itemContainers = _itemContainers;
        Array.Reverse(itemContainers);
    }

    public void UpdateInventoryUI()
    {
        foreach(ItemContainer itemContainer in itemContainers)
        {
            itemContainer.gameObject.SetActive(false);
        }
        ShowingInventory();
    }

    public void ItemContainerFocus()
    {
        if(UI.instance.tabUI.currentWindowLayer == 1) return;

        for(int i=0; i < Player1.instance.playerInventory.CurrentContainerSize; i++)
        {
            if(i == UI.instance.tabUI.currentItemindex)
            {
                itemContainers[i].SetFocus(true);
                if(Player1.instance.playerInventory.items[i] != null)
                {
                    UI.instance.tabUI.itemExplainUI.SetItemExplain(Player1.instance.playerInventory.items[i].ItemDescription[0]);
                    UI.instance.tabUI.itemExplainUI.SetItemName(Player1.instance.playerInventory.items[i].ItemName);
                }else
                {
                    UI.instance.tabUI.itemExplainUI.SetItemExplain(" ");
                    UI.instance.tabUI.itemExplainUI.SetItemName(" ");
                }
            }else
            {
                itemContainers[i].SetFocus(false);
            }
        }
    }

    public void ItemContainerFocusDirect(int num)
    {
        for(int i=0; i < Player1.instance.playerInventory.CurrentContainerSize; i++)
        {
            if(i == num)
            {
                itemContainers[i].SetFocus(true);
                if(Player1.instance.playerInventory.items[i] != null)
                {
                    UI.instance.tabUI.itemExplainUI.SetItemExplain(Player1.instance.playerInventory.items[i].ItemDescription[0]);
                    UI.instance.tabUI.itemExplainUI.SetItemName(Player1.instance.playerInventory.items[i].ItemName);
                }else
                {
                    UI.instance.tabUI.itemExplainUI.SetItemExplain(" ");
                    UI.instance.tabUI.itemExplainUI.SetItemName(" ");
                }
                
            }else
            {
                itemContainers[i].SetFocus(false);
            }
            
        }
    }

    public void ShowingInventory()
    {
        for(int i=0; i<Player1.instance.playerInventory.CurrentContainerSize; i++)
        {
            itemContainers[i].gameObject.SetActive(true);

            if(Player1.instance.playerInventory.isEquipped[i])
            {
                itemContainers[i].EquipImage.gameObject.SetActive(true);
            }else
            {
                itemContainers[i].EquipImage.gameObject.SetActive(false);
            }

            if(Player1.instance.playerInventory.items[i] != null)
            {
                itemContainers[i].itemImage.sprite = Player1.instance.playerInventory.items[i].ItemImage;

            }else
            {
                itemContainers[i].itemImage.sprite = nullSprite;
            }

            if(Player1.instance.playerInventory.itemsamount[i] > 1)
            {
                itemContainers[i].SetItemAmountUI(true);
                itemContainers[i].SetItemAmountText(Player1.instance.playerInventory.itemsamount[i].ToString());
            }else
            {
                itemContainers[i].SetItemAmountUI(false);
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


}
