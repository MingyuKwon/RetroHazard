using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerItemContainer : MonoBehaviour
{
    [SerializeField] int containerNum;

    PlayerStatus status;

    Image backGround;
    Image fadeImage;
    public Image itemImage;
    public Text itemAmount;
    public Image EquipImage;
    public FocusUI focus;

    BoxUI boxUI;
    PlayerItemUI playerItemUI;

    public GameObject focusSelectPanel;

    public bool isFocused = false;
    public bool isCombineable = false;

    public int indexLimitMin = 0;
    public int indexLimitMax = 2;

    public int selectIndex = 0;

    //for Code Struct
    public bool isPreviousEneterd;
    //for Code Struct

    private void Awake() {
        status = FindObjectOfType<PlayerStatus>();

        backGround = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        EquipImage = transform.GetChild(2).GetComponent<Image>();
        fadeImage = transform.GetChild(3).GetComponent<Image>();
        focus = transform.GetChild(4).GetComponent<FocusUI>();

        boxUI = transform.parent.transform.parent.GetComponent<BoxUI>();
        playerItemUI = GetComponentInParent<PlayerItemUI>();

        transform.GetChild(1).gameObject.SetActive(false);

        focusSelectPanel = focus.gameObject.transform.GetChild(0).gameObject;
        focusSelectPanel.SetActive(false);

    }

    private void OnEnable() {
        if(playerItemUI != null)
        {
            if(playerItemUI.playerInventory == null) return;
            if(playerItemUI.playerInventory.items[containerNum] == null) return;

            if(playerItemUI.playerInventory.items[containerNum].isKeyItem)
            {
                focus.SetselectText(2, "");
                indexLimitMax = 1;
            }else
            {
                focus.SetselectText(2, "Discard");
                indexLimitMax = 2;
            }

            if(playerItemUI.playerInventory.isEquipped[containerNum])
            {
                focus.SetselectText(0, "");
                indexLimitMin = 1;
            }else
            {
                focus.SetselectText(0, "Put item");
                indexLimitMin = 0;
            }
        }

        selectIndex = indexLimitMin;
    }

    private void Update() {
        CheckWindowLayer();
        CheckContainerFull();
    }

    private void CheckContainerFull()
    {
        if(playerItemUI.playerInventory == null) return;
        if(playerItemUI.playerInventory.items[containerNum] == null) return;

        if(playerItemUI.playerInventory.items[containerNum].isEnergy1)
        {
            if(playerItemUI.playerInventory.itemsamount[containerNum] == playerItemUI.playerInventory.Energy1BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }else if(playerItemUI.playerInventory.items[containerNum].isEnergy2)
        {
            if(playerItemUI.playerInventory.itemsamount[containerNum] == playerItemUI.playerInventory.Energy2BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(playerItemUI.playerInventory.items[containerNum].isEnergy3)
        {
            if(playerItemUI.playerInventory.itemsamount[containerNum] == playerItemUI.playerInventory.Energy3BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(playerItemUI.playerInventory.items[containerNum].isSheild)
        {
            if(playerItemUI.playerInventory.itemsamount[containerNum] == playerItemUI.playerInventory.SheildBatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }
    }

    private void CheckWindowLayer()
    {
        if(boxUI.currentWindowLayer == 0)
        {
            SetSelect(false);
            backGround.color = new Color(1f, 1f, 1f, 1f);
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
            isPreviousEneterd = false;

        }else if(boxUI.currentWindowLayer == 2)
        {
            SetSelect(false);
            if(!isPreviousEneterd)
            {
                isPreviousEneterd = true;

                bool flag = false;

                if(playerItemUI.playerInventory.items[containerNum] != null)
                {
                    if(boxUI.combineStartItem.isKeyItem && playerItemUI.playerInventory.items[containerNum].isKeyItem)
                    {
                        foreach(int itemCode in playerItemUI.playerInventory.items[containerNum].combineItems)
                        {
                            if(boxUI.combineStartItem.KeyItemCode == itemCode)
                            {
                                isCombineable = true;
                                flag = true;
                                break;
                            }

                        }
                    }else if(!boxUI.combineStartItem.isKeyItem && !playerItemUI.playerInventory.items[containerNum].isKeyItem)
                    {
                        foreach(int itemCode in playerItemUI.playerInventory.items[containerNum].combineItems)
                        {
                            if(boxUI.combineStartItem.NormalItemCode == itemCode)
                            {
                                isCombineable = true;
                                flag = true;
                                break;
                            }

                        }
                    }
                 
                }

                if(!flag)
                {
                    isCombineable = false;
                }

                if(containerNum == boxUI.combineStartItemIndex)
                {
                    isCombineable = false;
                }
            }

            if(isCombineable)
            {
                fadeImage.color = new Color(0f, 0f, 0f, 0f);
            }else
            {
                fadeImage.color = new Color(0f, 0f, 0f, 0.5f);
            }

            
        }else if(boxUI.currentWindowLayer == 1)
        {
            isPreviousEneterd = false;
            if(isFocused)
            {
                SetSelect(true);
                backGround.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                fadeImage.color = new Color(0f, 0f, 0f, 0f);
                focus.SetSelect(selectIndex); 
            }else
            {
                SetSelect(false);
                backGround.color = new Color(1f, 1f, 1f, 1f);
                fadeImage.color = new Color(0f, 0f, 0f, 0f);
            }
            
        }
    }

    public void SetSelectIndex(int index)
    {
        selectIndex = Mathf.Clamp(index, indexLimitMin, indexLimitMax);
    }


    public void SetItemAmountUI(bool flag)
    {
        transform.GetChild(1).gameObject.SetActive(flag);
    }

    public void SetItemAmountText(String str)
    {
        itemAmount.text = str;
    }

    private void SetSelect(bool flag)
    {
        focusSelectPanel.SetActive(flag);
    }

    public void SetFocus(bool flag)
    {
        focus.SetFocus(flag);
        isFocused = flag;
    }
}
