using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class TabUI : MonoBehaviour, CallBackInterface
{
    static public event Action<int> discardItemEvent;
    static public event Action<int, float> UsePotionEvent;
    static public event Action<InteractiveDialog, int> Interact_KeyItem_Success_Event; // success dialog and delete item from inventory

    static public event Action<ItemInformation , int , ItemInformation, int> CombineEvent; // Combine start Item, combine start index, select Item, selected index
    PlayerStatus status;

    public bool inputOk = false;

    public bool isShowing = false;
    public bool isInteractive = false; // true -> open when get item, interact with object , false -> open with menu
    public bool isOpenedItem = false; // true -> open when get item, false -> interact with object

    public bool isUseKeyItem = false; 
    public int[] neededKeyItemCode;

    private Player player;

    

    public int currentWindowLayer = 0; // 0 : normal, 1 : select, 2 : combine
    public int currentItemindex = 0;
    public int previousItemindex = 0;
    public bool yesNoChoice = true;

    Image background;
    ItemUI itemUI;
    ItemExplainUI itemExplainUI;
    CurrentGoalUI currentGoalUI;
    InteractiveMessageUI interactiveMessageUI;
    ItemObtainYesNoPanelUI itemObtainYesNoPanelUI;

    bulletItem bulletItem;
    KeyItem keyItem;
    PotionItem potionItem;
    ExpansionItem expansionItem;

    public ItemInformation combineStartItem = null;

    public InteractiveDialog interactiveDialog;
    public int combineStartItemIndex;

    public int discardTargetItemIndex;

    private void Awake() {
        status = FindObjectOfType<PlayerStatus>();
        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(EnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(BackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Back");
        player.AddInputEventDelegate(UpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");

        background = transform.GetChild(0).GetComponent<Image>();
        itemUI = GetComponentInChildren<ItemUI>();
        itemExplainUI = GetComponentInChildren<ItemExplainUI>();
        currentGoalUI = GetComponentInChildren<CurrentGoalUI>();
        interactiveMessageUI = GetComponentInChildren<InteractiveMessageUI>();
        itemObtainYesNoPanelUI = GetComponentInChildren<ItemObtainYesNoPanelUI>();
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(EnterPressed);
        player.RemoveInputEventDelegate(BackPressed);
        player.RemoveInputEventDelegate(UpPressed);
        player.RemoveInputEventDelegate(DownPressed);
        player.RemoveInputEventDelegate(RightPressed);
        player.RemoveInputEventDelegate(LeftPressed);
    }

    private void OnEnable() {
        IinteractiveUI.interact_Input_Rlease_Event += InputGetBack;
        currentWindowLayer = 0;
        transform.parent.GetComponent<UI>().MouseCursor(true);
        discardTargetItemIndex = -1;
    }

    private void OnDisable() {
        IinteractiveUI.interact_Input_Rlease_Event -= InputGetBack;
        currentWindowLayer = 0;
        transform.parent.GetComponent<UI>().MouseCursor(false);
        isUseKeyItem = false;
        discardTargetItemIndex = -1;
    }

    private void InputGetBack()
    {
        if(this.gameObject.activeInHierarchy != true) return;
        inputOk = true;
    }

    public void UpdateTabUI()
    {
        itemUI.UpdateInventoryUI();

        Debug.Log(PlayerGoalCollection.currentGoalIndex);
        currentGoalUI.ChangeGoalText(PlayerGoalCollection.PlayerGoals[PlayerGoalCollection.currentGoalIndex]);
    }

    // for question option select

    private void ContainerLimit()
    {
        currentItemindex = Mathf.Clamp(currentItemindex, 0, GameManagerUI.instance.CurrentContainer-1);
    }

    IEnumerator showInteractiveDialogDelay()
    {
        yield return new WaitForEndOfFrame();
        GameManagerUI.instance.VisualizeInteractiveUI(true);
        GameManagerUI.instance.Visualize_Tab_Interactive(false);
    }
    public void EnterPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isOpenedItem)
        {
            if(yesNoChoice)
            {
                if(itemUI.isInventoryFull)
                {
                    inputOk = false;
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
            if(itemUI.currentindex < 0) return;
            if(itemUI.playerInventory.items[itemUI.currentindex] == null) return;
                        
            if(currentWindowLayer == 0)
            {
                if(isUseKeyItem)
                {
                    if(itemUI.itemContainers[itemUI.currentindex].isInteractive)
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
                if(itemUI.itemContainers[itemUI.currentindex].selectIndex == 0) // use
                {
                    if(itemUI.itemContainers[itemUI.currentindex].focus.selectTexts[0].text == "DisArm")
                    {
                        int KeyItemCode = itemUI.playerInventory.items[itemUI.currentindex].KeyItemCode;
                        if(KeyItemCode < 3 || (KeyItemCode > 8 && KeyItemCode < 12) || (KeyItemCode > 11 && KeyItemCode < 15))
                        {
                            status.ChangeWeapon(0);
                            itemUI.itemContainers[itemUI.currentindex].focus.SetselectText(0, "Equip");
                        }else
                        {
                            status.ChangeSheild(3);
                            itemUI.itemContainers[itemUI.currentindex].focus.SetselectText(0, "Equip");
                        }

                    }else if(itemUI.itemContainers[itemUI.currentindex].focus.selectTexts[0].text == "Equip")
                    {
                        int KeyItemCode = itemUI.playerInventory.items[itemUI.currentindex].KeyItemCode;

                        if(KeyItemCode < 3)
                        {
                            status.ChangeWeapon(KeyItemCode + 1);
                            itemUI.itemContainers[itemUI.currentindex].focus.SetselectText(0, "DisArm");
                        }else if(KeyItemCode > 8 && KeyItemCode < 12)
                        {
                            status.ChangeWeapon(KeyItemCode - 8);
                            itemUI.itemContainers[itemUI.currentindex].focus.SetselectText(0, "DisArm");
                        }
                        else if(KeyItemCode > 11 && KeyItemCode < 15)
                        {
                            status.ChangeWeapon(KeyItemCode - 11);
                            itemUI.itemContainers[itemUI.currentindex].focus.SetselectText(0, "DisArm");
                        }
                        else if(KeyItemCode > 2 && KeyItemCode < 6)
                        {
                            status.ChangeSheild(KeyItemCode - 3);
                            itemUI.itemContainers[itemUI.currentindex].focus.SetselectText(0, "DisArm");
                        }

                        
                    }else if(itemUI.itemContainers[itemUI.currentindex].focus.selectTexts[0].text == "Use")
                    {
                        if(itemUI.playerInventory.items[itemUI.currentindex].isPotion)
                        {
                            UsePotionEvent.Invoke(itemUI.currentindex, itemUI.playerInventory.items[itemUI.currentindex].healAmount);
                        }

                        // enable to enter use is this item can be used in this situation
                        if(isUseKeyItem)
                        {
                            inputOk = false;
                            Interact_KeyItem_Success_Event.Invoke(interactiveDialog, itemUI.currentindex);
                            GameManagerUI.instance.SetInteractiveDialogText(interactiveDialog.SucessDialog);
                            StartCoroutine(showInteractiveDialogDelay());
                            
                        }
                    }

                    currentWindowLayer--;

                }else if(itemUI.itemContainers[itemUI.currentindex].selectIndex == 1) // combine
                {
                    combineStartItem = itemUI.playerInventory.items[itemUI.currentindex];
                    combineStartItemIndex = itemUI.currentindex;
                    previousItemindex = combineStartItemIndex;
                    currentWindowLayer++;
                }else if(itemUI.itemContainers[itemUI.currentindex].selectIndex == 2) // discard
                {
                    discardTargetItemIndex = itemUI.currentindex;
                    AlertUI.instance.ShowAlert("Are you sure to Discard this Item? \n\n <i>(discarded Item cannot be restored)</i>", this);
                }
            }
            else if(currentWindowLayer == 2)
            {
                if(!itemUI.itemContainers[itemUI.currentindex].isCombineable) return;

                CombineEvent.Invoke(combineStartItem, combineStartItemIndex, itemUI.playerInventory.items[itemUI.currentindex], itemUI.currentindex);
                currentWindowLayer--;
                currentWindowLayer--;
            }

            
        }
    }

    public void CallBack()
    {
        discardItemEvent.Invoke(discardTargetItemIndex);
        currentWindowLayer--;
        discardTargetItemIndex = -1;
    }

    public void BackPressed(InputActionEventData data)
    {
        if(!inputOk) return;

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
    public void UpPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isOpenedItem) return;

        if(currentWindowLayer == 0)
        {
            currentItemindex -= 4;
            ContainerLimit();
        }else if(currentWindowLayer == 1)
        {
            int n = itemUI.itemContainers[itemUI.currentindex].selectIndex;
            n--;
            itemUI.itemContainers[itemUI.currentindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
        }else  if(currentWindowLayer == 2)
        {
            currentItemindex -= 4;
            ContainerLimit();
        }
        
    }
    public void DownPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isOpenedItem) return;

        if(currentWindowLayer == 0)
        {
            currentItemindex += 4;
            ContainerLimit();
        }else if(currentWindowLayer == 1)
        {
            int n = itemUI.itemContainers[itemUI.currentindex].selectIndex;
            n++;
            itemUI.itemContainers[itemUI.currentindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
        }else  if(currentWindowLayer == 2)
        {
            currentItemindex += 4;
            ContainerLimit();
        }

    }
    public void RightPressed(InputActionEventData data)
    {
        if(!inputOk) return;

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
    public void LeftPressed(InputActionEventData data)
    {
        if(!inputOk) return;

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
    // for question option select

    ////////////// override start /////////////////////////////

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowing && flag) return;

        if(isUseKeyItem && !flag && GameManagerUI.instance.isInteractiveUIActive)
        {
            
        }else
        {
            Tab_Menu_ChangeInput_PauseGame(flag);
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

    ////////////// override end /////////////////////////////

    public void Visualize_Tab_Menu(bool flag)
    {
        if(isShowing && flag) return;

        Tab_Menu_ChangeInput_PauseGame(flag);

        isInteractive = false;
        isShowing = flag;

        background.gameObject.SetActive(flag);
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(false);
        itemObtainYesNoPanelUI.gameObject.SetActive(false);
    }

    ////////////////////override start//////////////////////////
    public void Visualize_Tab_Obtain(bool flag)
    {
        if(isShowing && flag) return;

        Tab_Menu_ChangeInput_PauseGame(flag);

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
    ////////////////////override end//////////////////////////

    private void Tab_Menu_ChangeInput_PauseGame(bool flag)
    {
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            GameMangerInput.instance.changePlayerInputRule(0);
        }

        inputOk = flag;

        GameManager.instance.SetPauseGame(flag);
    }
}
