using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int containerNum;

    PlayerStatus status;

    Image backGround;
    Image fadeImage;
    public Image itemImage;
    public Text itemAmount;
    public Image EquipImage;
    public FocusUI focus;

    TabUI tabUI;
    ItemUI itemUI;

    public GameObject focusSelectPanel;

    public bool isFocused = false;
    public bool isCombineable = false;

    public bool isInteractive = false;

    int indexLimitMin = 0;
    int indexLimitMax = 2;

    public int selectIndex = 0;

    //for Code Struct
    public bool isPreviousEneterd;
    //for Code Struct

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(tabUI.isOpenedItem) return;
        tabUI.currentItemindex = containerNum;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(tabUI.isOpenedItem) return;
        tabUI.currentItemindex = -1;
    }



    private void Awake() {
        status = FindObjectOfType<PlayerStatus>();

        backGround = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        EquipImage = transform.GetChild(2).GetComponent<Image>();
        fadeImage = transform.GetChild(3).GetComponent<Image>();
        focus = transform.GetChild(4).GetComponent<FocusUI>();

        tabUI = transform.parent.transform.parent.GetComponent<TabUI>();
        itemUI = GetComponentInParent<ItemUI>();

        transform.GetChild(1).gameObject.SetActive(false);

        focusSelectPanel = focus.gameObject.transform.GetChild(0).gameObject;
        focusSelectPanel.SetActive(false);
    }

    private void Update() {
        CheckWindowLayer();
        CheckContainerFull();
    }

    private void CheckContainerFull()
    {
        if(itemUI.playerInventory == null) return;
        if(itemUI.playerInventory.items[containerNum] == null) return;

        if(itemUI.playerInventory.items[containerNum].isEnergy1)
        {
            if(itemUI.playerInventory.itemsamount[containerNum] == itemUI.playerInventory.Energy1BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }else if(itemUI.playerInventory.items[containerNum].isEnergy2)
        {
            if(itemUI.playerInventory.itemsamount[containerNum] == itemUI.playerInventory.Energy2BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(itemUI.playerInventory.items[containerNum].isEnergy3)
        {
            if(itemUI.playerInventory.itemsamount[containerNum] == itemUI.playerInventory.Energy3BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(itemUI.playerInventory.items[containerNum].isSheild)
        {
            if(itemUI.playerInventory.itemsamount[containerNum] == itemUI.playerInventory.SheildBatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }
        

    }

    public void SetInteractFade()
    {
        if(itemUI.playerInventory == null) return;
        if(itemUI.playerInventory.items[containerNum] == null) return;
        if(!tabUI.isUseKeyItem) return;

            if(itemUI.playerInventory.items[containerNum].isKeyItem)
            {
                bool flag = false;
                foreach(int n in tabUI.neededKeyItemCode)
                {
                    if(n == itemUI.playerInventory.items[containerNum].KeyItemCode)
                    {
                        
                        flag = true;
                        break;
                    }
                }

                if(flag)
                {
                    isInteractive = true;
                }else
                {
                    isInteractive = false;
                }
                
            }else
            {
                isInteractive = false;
            }
    }

    private void OnEnable() {
        if(itemUI != null)
        {
            if(itemUI.playerInventory == null) return;

            if(itemUI.playerInventory.items[containerNum] == null) return;

            SetSelectText();


            if(itemUI.playerInventory.items[containerNum].isKeyItem)
            {
                if(tabUI.isInteractive && !itemUI.playerInventory.items[containerNum].isEquipItem )
                {
                    indexLimitMin = 0;
                    focus.selectButtons[0].gameObject.SetActive(true);
                    
                }else if(tabUI.isInteractive && itemUI.playerInventory.items[containerNum].isEquipItem)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                }
                else if(!tabUI.isInteractive && itemUI.playerInventory.items[containerNum].isEquipItem)
                {
                    indexLimitMin = 0;
                    focus.selectButtons[0].gameObject.SetActive(true);
                }else if(!tabUI.isInteractive && !itemUI.playerInventory.items[containerNum].isEquipItem)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                }
                
                indexLimitMax = 1;
                focus.selectButtons[2].gameObject.SetActive(false);
            }else
            {
                if(tabUI.isInteractive)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                    indexLimitMax = 1;
                    focus.selectButtons[2].gameObject.SetActive(false);
                }else
                {
                    if(itemUI.playerInventory.items[containerNum].isBullet)
                    {
                        indexLimitMin = 1;
                        focus.selectButtons[0].gameObject.SetActive(false);
                    }else
                    {
                        indexLimitMin = 0;
                        focus.selectButtons[0].gameObject.SetActive(true);
                    }
                    
                    indexLimitMax = 2;
                    focus.selectButtons[2].gameObject.SetActive(true);
                }
            }

        }

        selectIndex = indexLimitMin;
    }

    private void OnDisable() {
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    private void SetSelectText()
    {
            if(itemUI.playerInventory.items[containerNum].isEquipItem)
            {
                int KeyItemCode = itemUI.playerInventory.items[containerNum].KeyItemCode;

                if(KeyItemCode + 1 == status.Energy || KeyItemCode - 3 == status.Sheild || KeyItemCode - 8 == status.Energy || KeyItemCode - 11 == status.Energy) // If that Item is now equipped
                {
                    focus.SetselectText(0, "DisArm"); // If you want to modify this String, you should also modify TabUI pressEnter code
                }else
                {
                    focus.SetselectText(0, "Equip"); // If you want to modify this String, you should also modify TabUI pressEnter code
                }
                
            }else
            {
                focus.SetselectText(0, "Use"); // If you want to modify this String, you should also modify TabUI pressEnter code
            }
    }

    private void CheckWindowLayer()
    {
        if(tabUI.currentWindowLayer == 0)
        {
            SetSelect(false);
            backGround.color = new Color(1f, 1f, 1f, 1f);
            if(tabUI.isUseKeyItem)
            {
                 if(isInteractive)
                {
                    fadeImage.color = new Color(0f, 0f, 0f, 0f);
                }else
                {
                    fadeImage.color = new Color(0f, 0f, 0f, 0.5f);
                }
            }else
            {
                fadeImage.color = new Color(0f, 0f, 0f, 0f);
            }
           
            
            isPreviousEneterd = false;

        }else if(tabUI.currentWindowLayer == 2)
        {
            SetSelect(false);
            if(!isPreviousEneterd)
            {
                isPreviousEneterd = true;

                bool flag = false;

                if(itemUI.playerInventory.items[containerNum] != null)
                {
                    if(tabUI.combineStartItem.isKeyItem && itemUI.playerInventory.items[containerNum].isKeyItem)
                    {
                        if(itemUI.playerInventory.items[containerNum].combineItems == null)
                        {
                            Debug.Log(itemUI.playerInventory.items[containerNum].name + " doesnt have combineItems");
                        }else
                        {
                            foreach(int itemCode in itemUI.playerInventory.items[containerNum].combineItems)
                            {
                                if(tabUI.combineStartItem.KeyItemCode == itemCode)
                                {
                                    isCombineable = true;
                                    flag = true;
                                    break;
                                }

                            }
                        }

                        
                    }else if(!tabUI.combineStartItem.isKeyItem && !itemUI.playerInventory.items[containerNum].isKeyItem)
                    {
                        foreach(int itemCode in itemUI.playerInventory.items[containerNum].combineItems)
                        {
                            if(tabUI.combineStartItem.NormalItemCode == itemCode)
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

                if(containerNum == tabUI.combineStartItemIndex)
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

            
        }else if(tabUI.currentWindowLayer == 1)
        {
            isPreviousEneterd = false;
            if(isFocused)
            {
                SetSelect(true);
                backGround.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                focus.SetSelect(selectIndex); 
            }else
            {
                SetSelect(false);
                backGround.color = new Color(1f, 1f, 1f, 1f);
            }

            if(tabUI.isUseKeyItem)
            {
                 if(isInteractive)
                {
                    fadeImage.color = new Color(0f, 0f, 0f, 0f);
                }else
                {
                    fadeImage.color = new Color(0f, 0f, 0f, 0.5f);
                }
            }else
            {
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
