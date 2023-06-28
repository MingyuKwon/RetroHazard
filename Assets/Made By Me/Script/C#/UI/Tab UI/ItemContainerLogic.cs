using System;
using UnityEngine.UI;
using UnityEngine;

public class ItemContainerLogic 
{
    int containerNum;
    public int indexLimitMin = 0;
    public int indexLimitMax = 2;
    public int selectIndex = 0;

    public bool isFocused = false;
    public bool isCombineable = false;
    public bool isInteractive = false;


    public Image backGround;
    public Image itemImage;
    public Image EquipImage;
    public Image fadeImage;

    public GameObject itemAmount;
    public Text itemAmountText;

    public FocusUI focus;

    public GameObject focusSelectPanel;


    //for Code Struct
    public bool isPreviousEneterd;
    //for Code Struct

    public ItemContainerLogic(int containerNum , Image backGround, Image itemImage, Image EquipImage,
    GameObject itemAmount , Text itemAmountText, Image fadeImage, FocusUI focus, GameObject focusSelectPanel)
    {
        this.containerNum = containerNum;

        this.backGround = backGround;

        this.itemImage = itemImage;
        this.EquipImage = EquipImage;

        this.itemAmount = itemAmount;
        this.itemAmountText = itemAmountText;
        SetItemAmountUI(false); 

        this.fadeImage = fadeImage;
        this.focus = focus;

        this.focusSelectPanel = focusSelectPanel;
        this.focusSelectPanel.SetActive(false);
    }

    public void OnInventoryPointerEnter()
    {
        if(UI.instance.tabUI.isOpenedItem) return;
        if(UI.instance.tabUI.currentWindowLayer == 1) return;
        UI.instance.tabUI.currentItemindex = containerNum;
    }

    public void OnInventoryPointerExit()
    {
        if(UI.instance.tabUI.isOpenedItem) return;
        if(UI.instance.tabUI.currentWindowLayer == 1) return;
        UI.instance.tabUI.currentItemindex = -1;
    }


    public void OnBoxPointerEnter()
    {
        if(UI.instance.tabUI.currentWindowLayer == 1) return;
        UI.instance.boxUI.boxItemIndex = containerNum;
    }

    public void OnBoxPointerExit()
    {
        if(UI.instance.tabUI.currentWindowLayer == 1) return;
        UI.instance.boxUI.boxItemIndex = -1;
    }


    public void OnPlayerPointerEnter()
    {
        if(UI.instance.tabUI.currentWindowLayer == 1) return;
        UI.instance.boxUI.playerItemIndex = containerNum;
    }

    public void OnPlayerPointerExit()
    {
        if(UI.instance.tabUI.currentWindowLayer == 1) return;
        UI.instance.boxUI.playerItemIndex = -1;
    }

    public void UpdateInventoryItemContainer() {
        CheckTabWindowLayer();
        CheckInventoryContainerFull();
    }
    
    public void UpdatePlayerItemContainer() {
        CheckBoxPlayerWindowLayer();
        CheckInventoryContainerFull();
    }

    public void UpdateBoxItemContainer() {
        CheckBoxBoxWindowLayer();
        CheckBoxContainerFull();
    }

    public void OnEnableInventoryContainer() {
        if(UI.instance.tabUI.itemUI != null)
        {
            if(Player1.instance.playerInventory == null) return;

            if(Player1.instance.playerInventory.items[containerNum] == null) return;

            SetSelectText();


            if(Player1.instance.playerInventory.items[containerNum].isKeyItem)
            {
                if(UI.instance.tabUI.isInteractive && !Player1.instance.playerInventory.items[containerNum].isEquipItem )
                {
                    indexLimitMin = 0;
                    focus.selectButtons[0].gameObject.SetActive(true);
                    
                }else if(UI.instance.tabUI.isInteractive && Player1.instance.playerInventory.items[containerNum].isEquipItem)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                }
                else if(!UI.instance.tabUI.isInteractive && Player1.instance.playerInventory.items[containerNum].isEquipItem)
                {
                    indexLimitMin = 0;
                    focus.selectButtons[0].gameObject.SetActive(true);
                }else if(!UI.instance.tabUI.isInteractive && !Player1.instance.playerInventory.items[containerNum].isEquipItem)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                }
                
                indexLimitMax = 1;
                focus.selectButtons[2].gameObject.SetActive(false);
            }else
            {
                if(UI.instance.tabUI.isInteractive)
                {
                    indexLimitMin = 1;
                    focus.selectButtons[0].gameObject.SetActive(false);
                    indexLimitMax = 1;
                    focus.selectButtons[2].gameObject.SetActive(false);
                }else
                {
                    if(Player1.instance.playerInventory.items[containerNum].isBullet)
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

    public void OnEnablePlayerContainer() {
        if(UI.instance.boxUI.playerItemUI != null)
        {
            if(Player1.instance.playerInventory == null) return;
            if(Player1.instance.playerInventory.items[containerNum] == null) return;

            if(Player1.instance.playerInventory.items[containerNum].isKeyItem)
            {
                focus.SetselectText(2, "");
                indexLimitMax = 1;
                focus.selectButtons[2].gameObject.SetActive(false);
            }else
            {
                focus.SetselectText(2, "Discard");
                indexLimitMax = 2;
                focus.selectButtons[2].gameObject.SetActive(true);
            }

            if(Player1.instance.playerInventory.isEquipped[containerNum])
            {
                focus.SetselectText(0, "");
                indexLimitMin = 1;
                focus.selectButtons[0].gameObject.SetActive(false);
            }else
            {
                focus.SetselectText(0, "Put item");
                indexLimitMin = 0;
                focus.selectButtons[0].gameObject.SetActive(true);
            }
        }

        selectIndex = indexLimitMin;
    }

    public void OnEnableBoxContainer() {
        if(Player1.instance.playerItemBox == null) return;
        if(Player1.instance.playerItemBox.items[containerNum] == null) return;
        indexLimitMax = 0;
        indexLimitMin = 0;
        focus.selectButtons[1].gameObject.SetActive(false);
        focus.selectButtons[2].gameObject.SetActive(false);
        selectIndex = 0;
    }

    private void SetSelectText()
    {
        if(Player1.instance.playerInventory.items[containerNum].isEquipItem)
        {
            int KeyItemCode = Player1.instance.playerInventory.items[containerNum].KeyItemCode;

            if(KeyItemCode + 1 == Player1.instance.playerStatus.Energy || 
            KeyItemCode - 3 == Player1.instance.playerStatus.Sheild ||
            KeyItemCode - 8 == Player1.instance.playerStatus.Energy || 
            (KeyItemCode - 11 == Player1.instance.playerStatus.Energy && Player1.instance.playerStatus.Energy != 0)) // If that Item is now equipped
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


    public void OnDisable() {
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    private void CheckTabWindowLayer()
    {
        if(UI.instance.tabUI.currentWindowLayer == 0)
        {
            SetSelect(false);
            backGround.color = new Color(1f, 1f, 1f, 1f);
            if(UI.instance.tabUI.isUseKeyItem)
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

        }else if(UI.instance.tabUI.currentWindowLayer == 2)
        {
            SetSelect(false);
            if(!isPreviousEneterd)
            {
                isPreviousEneterd = true;

                bool flag = false;

                if(Player1.instance.playerInventory.items[containerNum] != null)
                {
                    if(UI.instance.tabUI.combineStartItem.isKeyItem 
                    && Player1.instance.playerInventory.items[containerNum].isKeyItem)
                    {
                        if(Player1.instance.playerInventory.items[containerNum].combineItems == null)
                        {
                            Debug.Log(Player1.instance.playerInventory.items[containerNum].name + " doesnt have combineItems");
                        }else
                        {
                            foreach(int itemCode in Player1.instance.playerInventory.items[containerNum].combineItems)
                            {
                                if(UI.instance.tabUI.combineStartItem.KeyItemCode == itemCode)
                                {
                                    isCombineable = true;
                                    flag = true;
                                    break;
                                }

                            }
                        }

                    }else if(!UI.instance.tabUI.combineStartItem.isKeyItem && !Player1.instance.playerInventory.items[containerNum].isKeyItem)
                    {
                        foreach(int itemCode in Player1.instance.playerInventory.items[containerNum].combineItems)
                        {
                            if(UI.instance.tabUI.combineStartItem.NormalItemCode == itemCode)
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

                if(containerNum == UI.instance.tabUI.combineStartItemIndex)
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

            
        }else if(UI.instance.tabUI.currentWindowLayer == 1)
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

            if(UI.instance.tabUI.isUseKeyItem)
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

    private void CheckBoxPlayerWindowLayer()
    {
        if(UI.instance.boxUI.currentWindowLayer == 0)
        {
            SetSelect(false);
            backGround.color = new Color(1f, 1f, 1f, 1f);
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
            isPreviousEneterd = false;

        }else if(UI.instance.boxUI.currentWindowLayer == 2)
        {
            SetSelect(false);
            if(!isPreviousEneterd)
            {
                isPreviousEneterd = true;

                bool flag = false;

                if(Player1.instance.playerInventory.items[containerNum] != null)
                {
                    if(UI.instance.boxUI.combineStartItem.isKeyItem && Player1.instance.playerInventory.items[containerNum].isKeyItem)
                    {
                        foreach(int itemCode in Player1.instance.playerInventory.items[containerNum].combineItems)
                        {
                            if(UI.instance.boxUI.combineStartItem.KeyItemCode == itemCode)
                            {
                                isCombineable = true;
                                flag = true;
                                break;
                            }

                        }
                    }else if(!UI.instance.boxUI.combineStartItem.isKeyItem && !Player1.instance.playerInventory.items[containerNum].isKeyItem)
                    {
                        foreach(int itemCode in Player1.instance.playerInventory.items[containerNum].combineItems)
                        {
                            if(UI.instance.boxUI.combineStartItem.NormalItemCode == itemCode)
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

                if(containerNum == UI.instance.boxUI.combineStartItemIndex)
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

            
        }else if(UI.instance.boxUI.currentWindowLayer == 1)
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



    private void CheckBoxBoxWindowLayer()
    {
        if(UI.instance.boxUI.currentWindowLayer == 0)
        {
            SetSelect(false);
            backGround.color = new Color(1f, 1f, 1f, 1f);
            fadeImage.color = new Color(0f, 0f, 0f, 0f);

        }else if(UI.instance.boxUI.currentWindowLayer == 1)
        {
            if(isFocused && UI.instance.boxUI.isBox)
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

    public void CheckInventoryContainerFull()
    {
        if(Player1.instance.playerInventory == null) return;
        if(Player1.instance.playerInventory.items[containerNum] == null) return;

        if(Player1.instance.playerInventory.items[containerNum].isEnergy1)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.Energy1BatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }
        }else if(Player1.instance.playerInventory.items[containerNum].isEnergy2)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.Energy2BatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }

        }else if(Player1.instance.playerInventory.items[containerNum].isEnergy3)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.Energy3BatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }

        }else if(Player1.instance.playerInventory.items[containerNum].isSheild)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.SheildBatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }
        }
    }

    public void CheckBoxContainerFull()
    {
        if(Player1.instance.playerItemBox == null) return;
        if(Player1.instance.playerItemBox.items[containerNum] == null) return;

        if(Player1.instance.playerItemBox.items[containerNum].isEnergy1)
        {
            if(Player1.instance.playerItemBox.itemsamount[containerNum] == 
            Player1.instance.playerStatus.Energy1BatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }
        }else if(Player1.instance.playerItemBox.items[containerNum].isEnergy2)
        {
            if(Player1.instance.playerItemBox.itemsamount[containerNum] == 
            Player1.instance.playerStatus.Energy2BatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }

        }else if(Player1.instance.playerItemBox.items[containerNum].isEnergy3)
        {
            if(Player1.instance.playerItemBox.itemsamount[containerNum] == 
            Player1.instance.playerStatus.Energy3BatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }

        }else if(Player1.instance.playerItemBox.items[containerNum].isSheild)
        {
            if(Player1.instance.playerItemBox.itemsamount[containerNum] == 
            Player1.instance.playerStatus.SheildBatteryLimit)
            {
                itemAmountText.color = Color.green;
            }else
            {
                itemAmountText.color = Color.white;
            }
        }
    }

    public void SetInteractFade()
    {
        if(Player1.instance.playerInventory == null) return;
        if(Player1.instance.playerInventory.items[containerNum] == null) return;
        if(!UI.instance.tabUI.isUseKeyItem) return;

        if(Player1.instance.playerInventory.items[containerNum].isKeyItem)
        {
            bool flag = false;
            foreach(int n in UI.instance.tabUI.neededKeyItemCode)
            {
                if(n == Player1.instance.playerInventory.items[containerNum].KeyItemCode)
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

    public void SetSelectIndex(int index)
    {
        selectIndex = Mathf.Clamp(index, indexLimitMin, indexLimitMax);
    }


    public void SetItemAmountUI(bool flag)
    {
        itemAmount.SetActive(flag);
    }

    public void SetItemAmountText(String str)
    {
        itemAmountText.text = str;
    }

    public void SetSelect(bool flag)
    {
        focusSelectPanel.SetActive(flag);
    }

    public void SetFocus(bool flag)
    {
        focus.SetFocus(flag);
        isFocused = flag;
    }
}
