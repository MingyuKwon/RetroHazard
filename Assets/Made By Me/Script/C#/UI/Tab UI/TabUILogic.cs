using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TabUILogic : CallBackInterface
{
    public bool isShowing = false;
    public bool isInteractive = false; 
    // true -> open when get item, interact with object , false -> open with menu
    public bool isOpenedItem = false; 
    // true -> open when get item, false -> interact with object

    public bool isUseKeyItem = false; 
    public int[] neededKeyItemCode;

    public int currentWindowLayer = 0; // 0 : normal, 1 : select, 2 : combine
    public int currentItemindex = 0;
    public int previousItemindex = 0;
    public bool yesNoChoice = true;

    public ItemInformation combineStartItem = null;
    public int combineStartItemIndex;
    public int discardTargetItemIndex;


    bulletItem bulletItem;
    KeyItem keyItem;
    PotionItem potionItem;
    ExpansionItem expansionItem;

    InteractiveDialog interactiveDialog;
    
    public ItemUI itemUI;
    public ItemExplainUI itemExplainUI;
    public CurrentGoalUI currentGoalUI;
    public InteractiveMessageUI interactiveMessageUI;
    public ItemObtainYesNoPanelUI itemObtainYesNoPanelUI;
    public MiniMap miniMap;


    Color selectColor = new Color(1.0f,1.0f,1.0f,1);
    Color unSelectColor = new Color(0.5f,0.5f,0.5f,0.5f); 

    TabUI monoBehavior;

    Image background;

    public int showingTabIndex{
        get{
            return _showingTabIndex;
        }

        set{
            _showingTabIndex = value;
            if(_showingTabIndex == 0)
            {
                showingTabItemImage.color = selectColor;
                showingTabMinimapImage.color = unSelectColor;

                itemUI.gameObject.SetActive(true);
                itemExplainUI.gameObject.SetActive(true);
                miniMap.gameObject.SetActive(false);

                GameManager.EventManager.InvokeShowNotice("TabUI", "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : \nspace\n\n<b>BACK</b> : \nbackSpace" , 200 ,250);

            }else if(_showingTabIndex == 1)
            {
                showingTabItemImage.color = unSelectColor;
                showingTabMinimapImage.color = selectColor;

                itemUI.gameObject.SetActive(false);
                itemExplainUI.gameObject.SetActive(false);
                miniMap.gameObject.SetActive(true);

                GameManager.EventManager.InvokeShowNotice("TabUI", "<i><b>-Input-</b></i>\n\n<b>BACK</b> : \nbackSpace" , 200 ,250);

            }
        }
    }

    int _showingTabIndex = 0;

    Image showingTabItemImage;
    Image showingTabMinimapImage;

    public TabUILogic(Image background, TabUI tabUI)
    {
        itemUI = UI.instance.tabUI.itemUI;
        itemExplainUI = UI.instance.tabUI.itemExplainUI;
        currentGoalUI = UI.instance.tabUI.currentGoalUI;
        interactiveMessageUI = UI.instance.tabUI.interactiveMessageUI;
        itemObtainYesNoPanelUI = UI.instance.tabUI.itemObtainYesNoPanelUI;
        miniMap = UI.instance.tabUI.miniMap;

        monoBehavior = tabUI;

        this.background = background;

        showingTabItemImage = tabUI.gameObject.transform.GetChild(7).GetChild(0).GetComponent<Image>();
        showingTabMinimapImage = tabUI.gameObject.transform.GetChild(7).GetChild(1).GetComponent<Image>();
    }


    public void Visualize_Tab_Menu(bool flag)
    {
        if(isShowing && flag) return;

        GameManager.instance.SetPauseGame(flag);

        isInteractive = false;
        isShowing = flag;

        background.gameObject.SetActive(flag);
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(false);
        itemObtainYesNoPanelUI.gameObject.SetActive(false);
    }

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowing && flag) return;

        if(isUseKeyItem && !flag && GameManagerUI.instance.isInteractiveUIActive)
        {
            
        }else
        {
            GameManager.instance.SetPauseGame(flag);
        }
        
        isUseKeyItem = flag;

        interactiveMessageUI.images[0].gameObject.SetActive(false);
        interactiveMessageUI.gameObject.GetComponent<Image>().color = new Color(100f / 255f, 150f/ 255f, 200f/ 255f, 220f/ 255f);
        

        isInteractive = true;
        isShowing = flag;
        background.gameObject.SetActive(flag);
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(false);
        itemObtainYesNoPanelUI.gameObject.SetActive(false);

    }

    public void Visualize_Tab_Interactive(bool flag , InteractiveDialog dialog)
    {
        Visualize_Tab_Interactive(flag);

        if(flag)
        {
            neededKeyItemCode = dialog.InteractKeyItems;
            interactiveDialog = dialog;
        }else
        {
            neededKeyItemCode = null;
            interactiveDialog = null;
        }

        itemUI.SetInteractFade();

        interactiveMessageUI.SetInteractiveName(dialog.Interactive_name);
        interactiveMessageUI.SetInteractiveSituation(dialog.Interactive_Situation);

    }

    public void Visualize_Tab_Obtain(bool flag)
    {
        if(isShowing && flag) return;

        GameManager.instance.SetPauseGame(flag);

        interactiveMessageUI.images[0].gameObject.SetActive(true);

        interactiveMessageUI.gameObject.GetComponent<Image>().color = new Color(166f/ 255f, 166f/ 255f, 166f/ 255f, 220f/ 255f);

        if(flag)
        {
            currentItemindex = -1;
        }else
        {
            currentItemindex = 0;
        }

        isInteractive = true;
        isOpenedItem = flag;
        isShowing = flag;

        background.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(flag);
        itemObtainYesNoPanelUI.gameObject.SetActive(flag);
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(false);
        currentGoalUI.gameObject.SetActive(false);
        
    }

    public void Visualize_Tab_Obtain(bool flag , bulletItem item)
    {
        Visualize_Tab_Obtain(flag);
        bulletItem = item;

        interactiveMessageUI.SetInteractiveName(item.information.ItemName);
        interactiveMessageUI.SetInteractiveSituation(item.information.ItemDescription[0]);
        interactiveMessageUI.SetItemImage(item.information.ItemImage);
        interactiveMessageUI.SetAmountText(item.information.amount);
    }

    public void Visualize_Tab_Obtain(bool flag , PotionItem item)
    {
        Visualize_Tab_Obtain(flag);
        potionItem = item;

        interactiveMessageUI.SetInteractiveName(item.information.ItemName);
        interactiveMessageUI.SetInteractiveSituation(item.information.ItemDescription[0]);
        interactiveMessageUI.SetItemImage(item.information.ItemImage);
        interactiveMessageUI.SetAmountText(item.information.amount);
    }

    public void Visualize_Tab_Obtain(bool flag , KeyItem item)
    {
        Visualize_Tab_Obtain(flag);
        keyItem = item;

        interactiveMessageUI.SetInteractiveName(item.information.ItemName);
        interactiveMessageUI.SetInteractiveSituation(item.information.ItemDescription[0]);
        interactiveMessageUI.SetItemImage(item.information.ItemImage);
        interactiveMessageUI.SetAmountText(item.information.amount);
        
    }

    public void OnEnable() {
        GameMangerInput.getInput(InputType.TabUIInput);

        currentWindowLayer = 0;
        UI.instance.SetMouseCursorActive(true);
        discardTargetItemIndex = -1;

        //////////////////////
        showingTabIndex = 0;
        //////////////////////////////////////////

        GameMangerInput.InputEvent.TabUIEnterPressed += UIEnterPressed;
        GameMangerInput.InputEvent.TabUIBackPressed += UIBackPressed;
        GameMangerInput.InputEvent.TabUIUpPressed += UIUpPressed;
        GameMangerInput.InputEvent.TabUIDownPressed += UIDownPressed;
        GameMangerInput.InputEvent.TabUIRightPressed += UIRightPressed;
        GameMangerInput.InputEvent.TabUILeftPressed += UILeftPressed;

        GameMangerInput.InputEvent.TabUILeftTabPressed += TabUILeftTabPressed;
        GameMangerInput.InputEvent.TabUIRightTabPressed += TabUIRightTabPressed;
    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.TabUIInput);

        currentWindowLayer = 0;
        UI.instance.SetMouseCursorActive(false);
        isUseKeyItem = false;
        discardTargetItemIndex = -1;

        showingTabIndex = 0;
        
        GameMangerInput.InputEvent.TabUIEnterPressed -= UIEnterPressed;
        GameMangerInput.InputEvent.TabUIBackPressed -= UIBackPressed;
        GameMangerInput.InputEvent.TabUIUpPressed -= UIUpPressed;
        GameMangerInput.InputEvent.TabUIDownPressed -= UIDownPressed;
        GameMangerInput.InputEvent.TabUIRightPressed -= UIRightPressed;
        GameMangerInput.InputEvent.TabUILeftPressed -= UILeftPressed;

        GameMangerInput.InputEvent.TabUILeftTabPressed -= TabUILeftTabPressed;
        GameMangerInput.InputEvent.TabUIRightTabPressed -= TabUIRightTabPressed;

        Button button1 = showingTabItemImage.gameObject.GetComponent<Button>();
        button1.onClick.RemoveAllListeners();

        Button button2 = showingTabMinimapImage.gameObject.GetComponent<Button>();
        button2.onClick.RemoveAllListeners();

        GameManager.EventManager.InvokeShowNotice("TabUI");
    }

    public void TabUILeftTabPressed()
    {
        showingTabIndex = 0;
    }

    public void TabUIRightTabPressed()
    {
        showingTabIndex = 1;
    }

    public int isMouseOnshowingTabIndex = -1;

    public void UIEnterPressed()
    {
        if(isMouseOnshowingTabIndex != -1)
        {
            showingTabIndex = isMouseOnshowingTabIndex;
            return;
        }

        if(isOpenedItem)
        {
            if(yesNoChoice)
            {
                if(Player1.instance.playerInventory.isInventoryFull)
                {
                    GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your inventory is full!</color></b> \n\n\nCan't get this item"});
                    GameManagerUI.instance.VisualizeInteractiveUI(true);
                    return;
                }

                GameManagerUI.instance.Visualize_Tab_Obtain(false);

                if(bulletItem != null)
                {
                    bulletItem.ObtainBulletItem();
                }else if(potionItem != null)
                {
                    potionItem.ObtainPotionItem();
                }else if(keyItem != null)
                {
                    keyItem.EventInvokeOverride();
                }

                
            }else
            {
                yesNoChoice = true;
                if(keyItem != null)
                {
                    keyItem.SetSpriteSortNoraml();
                    keyItem.interact.SetCheckActive(true);
                }
                GameManagerUI.instance.Visualize_Tab_Obtain(false);
            }

            bulletItem = null;
            keyItem = null;
            potionItem = null;
        }else
        {
            if(currentItemindex < 0) return;
            if(Player1.instance.playerInventory.items[currentItemindex] == null) return;
                        
            if(currentWindowLayer == 0)
            {
                if(isUseKeyItem)
                {
                    if(itemUI.itemContainers[currentItemindex].isInteractive)
                    {
                        currentWindowLayer++;
                    }
                }else
                {
                    currentWindowLayer++;
                }
                
            }
            else if(currentWindowLayer == 1)
            {
                if(itemUI.itemContainers[currentItemindex].selectIndex == 0) // use
                {
                    if(itemUI.itemContainers[currentItemindex].focus.selectTexts[0].text == "DisArm")
                    {
                        int KeyItemCode = Player1.instance.playerInventory.items[currentItemindex].KeyItemCode;
                        if(KeyItemCode < 3 || (KeyItemCode > 8 && KeyItemCode < 12) || (KeyItemCode > 11 && KeyItemCode < 15))
                        {
                            Player1.instance.playerStatus.ChangeWeapon(0);
                            itemUI.itemContainers[currentItemindex].focus.SetselectText(0, "Equip");
                        }else
                        {
                            Player1.instance.playerStatus.ChangeSheild(3);
                            itemUI.itemContainers[currentItemindex].focus.SetselectText(0, "Equip");
                        }

                    }else if(itemUI.itemContainers[currentItemindex].focus.selectTexts[0].text == "Equip")
                    {
                        int KeyItemCode = Player1.instance.playerInventory.items[currentItemindex].KeyItemCode;
                        if(KeyItemCode < 3)
                        {
                            Player1.instance.playerStatus.ChangeWeapon(KeyItemCode + 1);
                            itemUI.itemContainers[currentItemindex].focus.SetselectText(0, "DisArm");
                        }else if(KeyItemCode > 8 && KeyItemCode < 12)
                        {
                            
                            Player1.instance.playerStatus.ChangeWeapon(KeyItemCode - 8);
                            itemUI.itemContainers[currentItemindex].focus.SetselectText(0, "DisArm");
                        }
                        else if(KeyItemCode > 11 && KeyItemCode < 15)
                        {
                            Player1.instance.playerStatus.ChangeWeapon(KeyItemCode - 11);
                            itemUI.itemContainers[currentItemindex].focus.SetselectText(0, "DisArm");
                        }
                        else if(KeyItemCode > 2 && KeyItemCode < 6)
                        {
                            Player1.instance.playerStatus.ChangeSheild(KeyItemCode - 3);
                            itemUI.itemContainers[currentItemindex].focus.SetselectText(0, "DisArm");
                        }

                        
                    }else if(itemUI.itemContainers[currentItemindex].focus.selectTexts[0].text == "Use")
                    {
                        // 포션을 사용한 경우
                        if(Player1.instance.playerInventory.items[currentItemindex].isPotion)
                        {
                            GameManager.EventManager.Invoke_UsePotionEvent(currentItemindex, 
                                                                        Player1.instance.playerInventory.items[currentItemindex].healAmount);
                        }

                        // 상호작용 창에서 키 아이템을 사용한 경우
                        if(isUseKeyItem)
                        {
                            GameManager.EventManager.Invoke_Interact_KeyItem_Success_Event(interactiveDialog, currentItemindex);
                            GameManagerUI.instance.SetInteractiveDialogText(interactiveDialog.SucessDialog);
                            monoBehavior.StartCoroutine(showInteractiveDialogDelay());
                        }
                    }

                    currentWindowLayer--;

                }else if(itemUI.itemContainers[currentItemindex].selectIndex == 1) // combine
                {
                    combineStartItem = Player1.instance.playerInventory.items[currentItemindex];
                    combineStartItemIndex = currentItemindex;
                    previousItemindex = combineStartItemIndex;
                    currentWindowLayer++;
                }else if(itemUI.itemContainers[currentItemindex].selectIndex == 2) // discard
                {
                    discardTargetItemIndex = currentItemindex;
                    AlertUI.instance.ShowAlert("Are you sure to Discard this Item? \n\n <i>(discarded Item cannot be restored)</i>", this);
                }
            }
            else if(currentWindowLayer == 2)
            {
                if(!itemUI.itemContainers[currentItemindex].isCombineable) return;

                GameManager.EventManager.Invoke_CombineEvent(combineStartItem, 
                                                                combineStartItemIndex, 
                                                                Player1.instance.playerInventory.items[currentItemindex], 
                                                                currentItemindex);

                currentWindowLayer--;
                currentWindowLayer--;
            }

        }
    }
    IEnumerator showInteractiveDialogDelay()
    {
        yield return new WaitForEndOfFrame();
        GameManagerUI.instance.VisualizeInteractiveUI(true);
        GameManagerUI.instance.Visualize_Tab_Interactive(false);
    }

    public void CallBack()
    {
        GameManager.EventManager.Invoke_discardItemEvent(discardTargetItemIndex);
        currentWindowLayer--;
        discardTargetItemIndex = -1;
    }

    public void UpdateTabUI()
    {
        itemUI.UpdateInventoryUI();
        currentGoalUI.ChangeGoalText(PlayerGoalCollection.PlayerGoals[PlayerGoalCollection.currentGoalIndex]);
    }



    private void ContainerLimit()
    {
        currentItemindex = Mathf.Clamp(currentItemindex, 0, GameManagerUI.CurrentContainer-1);
    }

    public void UIBackPressed()
    {
        currentWindowLayer--;

        if(currentWindowLayer == 1)
        {   
            itemUI.ItemContainerFocusDirect(previousItemindex);
        }
        
        if(currentWindowLayer < 0)
        {
            currentWindowLayer = 0;
            if(isInteractive) 
            {
                if(isOpenedItem)
                {
                    yesNoChoice = true;
                    if(keyItem != null)
                    {
                        keyItem.SetSpriteSortNoraml();
                    }
                    GameManagerUI.instance.Visualize_Tab_Obtain(false);
                }else
                {
                    GameManagerUI.instance.Visualize_Tab_Interactive(false);
                }
                
            }else
            {
                GameManagerUI.instance.Visualize_Tab_Menu(false);
            }
            bulletItem = null;
            keyItem = null;
        }

    }
    public void UIUpPressed()
    {
        if(isOpenedItem) return;

        if(currentWindowLayer == 0)
        {
            currentItemindex -= 4;
            ContainerLimit();
        }else if(currentWindowLayer == 1)
        {
            if(currentItemindex < 0)
            {
                currentItemindex = 0;
            }

            int n = itemUI.itemContainers[currentItemindex].selectIndex;
            n--;
            itemUI.itemContainers[currentItemindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
        }else  if(currentWindowLayer == 2)
        {
            currentItemindex -= 4;
            ContainerLimit();
        }
        
    }
    public void UIDownPressed()
    {
        if(isOpenedItem) return;

        if(currentWindowLayer == 0)
        {
            currentItemindex += 4;
            ContainerLimit();
        }else if(currentWindowLayer == 1)
        {
            if(currentItemindex < 0)
            {
                currentItemindex = 0;
            }
            int n = itemUI.itemContainers[currentItemindex].selectIndex;
            n++;
            itemUI.itemContainers[currentItemindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
        }else  if(currentWindowLayer == 2)
        {
            currentItemindex += 4;
            ContainerLimit();
        }

    }
    public void UIRightPressed()
    {
        if(isOpenedItem)
        {
            yesNoChoice = false;
            return;
        }

        if(currentWindowLayer == 0)
        {
            currentItemindex++;
            ContainerLimit();
        }else if(currentWindowLayer == 1)
        {

        }else if(currentWindowLayer == 2)
        {
            currentItemindex++;
            ContainerLimit();
        }
        
    }
    public void UILeftPressed()
    {
        if(isOpenedItem)
        {
            yesNoChoice = true;
            return;
        }

        if(currentWindowLayer == 0)
        {
            currentItemindex--;
            ContainerLimit();
        }else if(currentWindowLayer == 1)
        {

        }else  if(currentWindowLayer == 2)
        {
            currentItemindex--;
            ContainerLimit();
        }
    }

}
