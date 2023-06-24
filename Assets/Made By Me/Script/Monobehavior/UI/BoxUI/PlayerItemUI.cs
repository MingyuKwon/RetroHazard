using System;
using UnityEngine;
using UnityEngine.UI;

///////////// Cleared ////////////////////////
public class PlayerItemUI : MonoBehaviour
{
    [SerializeField] Sprite nullSprite;

    public PlayerItemContainer[] itemContainers;
    public Image backgroundPanel;
 
    public Color selectColor = new Color(0.8f,0.8f,0.8f,1);
    public Color unSelectColor = new Color(0.5f,0.5f,0.5f,1);

    private void Awake() {
        itemContainers = GetComponentsInChildren<PlayerItemContainer>();
        Array.Reverse(itemContainers);

        backgroundPanel = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start() {
        UpdateInventoryUI();
    }

    private void OnEnable() {
        UpdateInventoryUI();
    }

    private void Update() {
        ItemContainerFocus();
    }

    public void UpdateInventoryUI()
    {
        foreach(PlayerItemContainer itemContainer in itemContainers)
        {
            itemContainer.gameObject.SetActive(false);
        }
        ShowingInventory();
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

    private void ItemContainerFocus()
    {
        if(UI.instance.boxUI.currentWindowLayer == 1) return;

        for(int i=0; i < Player1.instance.playerInventory.CurrentContainerSize; i++)
        {
            if(i == UI.instance.boxUI.playerItemIndex)
            {
                itemContainers[i].SetFocus(true);
                if(Player1.instance.playerInventory.items[i] != null)
                {
                    UI.instance.boxUI.boxitemExplainUI.SetItemExplain(Player1.instance.playerInventory.items[i].ItemDescription[0]);
                    UI.instance.boxUI.boxitemExplainUI.SetItemName(Player1.instance.playerInventory.items[i].ItemName);
                }else
                {
                    UI.instance.boxUI.boxitemExplainUI.SetItemExplain(" ");
                    UI.instance.boxUI.boxitemExplainUI.SetItemName(" ");
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
                    UI.instance.boxUI.boxitemExplainUI.SetItemExplain(Player1.instance.playerInventory.items[i].ItemDescription[0]);
                    UI.instance.boxUI.boxitemExplainUI.SetItemName(Player1.instance.playerInventory.items[i].ItemName);
                }else
                {
                    UI.instance.boxUI.boxitemExplainUI.SetItemExplain(" ");
                    UI.instance.boxUI.boxitemExplainUI.SetItemName(" ");
                }
                
            }else
            {
                itemContainers[i].SetFocus(false);
            }
            
        }
    }
}
