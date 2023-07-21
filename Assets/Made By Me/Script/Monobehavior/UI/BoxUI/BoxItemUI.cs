using System;
using UnityEngine;
using UnityEngine.UI;

public class BoxItemUI : MonoBehaviour
{
    [SerializeField] Sprite nullSprite;
    public BoxItemContainer[] itemContainers;
    // player Box 

    public Image backgroundPanel;
    public Color selectColor = new Color(0.8f,0.8f,0.8f,1);
    public Color unSelectColor = new Color(0.5f,0.5f,0.5f,1);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               


    private void Awake() {
        Debug.Log("Awake Awake Awake");
        itemContainers = GetComponentsInChildren<BoxItemContainer>();
        Array.Reverse(itemContainers);

        backgroundPanel = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start() {
        UpdateBoxItemUI();
    }

    private void Update() {
        ItemContainerFocus();
    }

    private void ItemContainerFocus()
    {
        if(UI.instance.boxUI.currentWindowLayer == 1) return;

        for(int i=0; i < 16; i++)
        {
            if(i == UI.instance.boxUI.boxItemIndex)
            {
                itemContainers[i].SetFocus(true);
                if(Player1.instance.playerItemBox.items[i] != null)
                {
                    UI.instance.boxUI.boxitemExplainUI.SetItemExplain(Player1.instance.playerItemBox.items[i].ItemDescription[0]);
                    UI.instance.boxUI.boxitemExplainUI.SetItemName(Player1.instance.playerItemBox.items[i].ItemName);
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
        for(int i=0; i < 16; i++)
        {
            if(i == num)
            {
                itemContainers[i].SetFocus(true);
                if(Player1.instance.playerItemBox.items[i] != null)
                {
                    UI.instance.boxUI.boxitemExplainUI.SetItemExplain(Player1.instance.playerItemBox.items[i].ItemDescription[0]);
                    UI.instance.boxUI.boxitemExplainUI.SetItemName(Player1.instance.playerItemBox.items[i].ItemName);
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

    public void UpdateBoxItemUI()
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

            if(Player1.instance.playerItemBox.items[i] != null)
            {
                itemContainers[i].itemImage.sprite = Player1.instance.playerItemBox.items[i].ItemImage;

            }else
            {
                itemContainers[i].itemImage.sprite = nullSprite;
            }

            if(Player1.instance.playerItemBox.itemsamount[i] > 1)
            {
                itemContainers[i].SetItemAmountUI(true);
                itemContainers[i].SetItemAmountText(Player1.instance.playerItemBox.itemsamount[i].ToString());
            }else
            {
                itemContainers[i].SetItemAmountUI(false);
            }
            
        }
    }
}
