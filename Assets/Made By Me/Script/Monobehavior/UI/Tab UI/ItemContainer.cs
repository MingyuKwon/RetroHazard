using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// find나 getComponent를 이용한 참조를 최대한 없앰

public class ItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int containerNum;

    Image backGround;
    Image fadeImage;
    public Image itemImage;
    public Text itemAmount;
    public Image EquipImage;
    public FocusUI focus;

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
        if(UI.instance.tabUI.isOpenedItem) return;
        UI.instance.tabUI.currentItemindex = containerNum;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(UI.instance.tabUI.isOpenedItem) return;
        UI.instance.tabUI.currentItemindex = -1;
    }


    private void Awake() {
        backGround = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponentInChildren<Text>();
        EquipImage = transform.GetChild(2).GetComponent<Image>();
        fadeImage = transform.GetChild(3).GetComponent<Image>();
        focus = transform.GetChild(4).GetComponent<FocusUI>();

        transform.GetChild(1).gameObject.SetActive(false);

        focusSelectPanel = focus.gameObject.transform.GetChild(0).gameObject;
        focusSelectPanel.SetActive(false);
    }

    private void Update() {
        CheckWindowLayer();
        CheckContainerFull();
    }

    private void CheckWindowLayer()
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

    private void CheckContainerFull()
    {
        if(Player1.instance.playerInventory == null) return;
        if(Player1.instance.playerInventory.items[containerNum] == null) return;

        if(Player1.instance.playerInventory.items[containerNum].isEnergy1)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.Energy1BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }
        }else if(Player1.instance.playerInventory.items[containerNum].isEnergy2)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.Energy2BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(Player1.instance.playerInventory.items[containerNum].isEnergy3)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.Energy3BatteryLimit)
            {
                itemAmount.color = Color.green;
            }else
            {
                itemAmount.color = Color.white;
            }

        }else if(Player1.instance.playerInventory.items[containerNum].isSheild)
        {
            if(Player1.instance.playerInventory.itemsamount[containerNum] == Player1.instance.playerInventory.SheildBatteryLimit)
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

    private void OnEnable() {
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

    private void OnDisable() {
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
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
