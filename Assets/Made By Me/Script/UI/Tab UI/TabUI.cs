using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class TabUI : MonoBehaviour
{
    static public event Action<int> discardItemEvent;

    public bool inputOk = false;

    public bool isShowing = false;
    public bool isInteractive = false; // true -> open when get item, interact with object , false -> open with menu
    public bool isOpenedItem = false; // true -> open when get item, false -> interact with object

    private Player player;

    

    public int currentWindowLayer = 0; // 0 : normal, 1 : select, 2 : combine
    public int currentItemindex = 0;
    public bool yesNoChoice = true;

    Image background;
    ItemUI itemUI;
    ItemExplainUI itemExplainUI;
    CurrentGoalUI currentGoalUI;
    InteractiveMessageUI interactiveMessageUI;
    EquippedUI equippedUI;
    ItemObtainYesNoPanelUI itemObtainYesNoPanelUI;

    bulletItem bulletItem;
    KeyItem keyItem;
    ExpansionItem expansionItem;

    private void Awake() {
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
        equippedUI = GetComponentInChildren<EquippedUI>();
        itemObtainYesNoPanelUI = GetComponentInChildren<ItemObtainYesNoPanelUI>();
    }

    private void OnEnable() {
        IinteractiveUI.interact_Input_Rlease_Event += InputGetBack;
    }

    private void OnDisable() {
        IinteractiveUI.interact_Input_Rlease_Event -= InputGetBack;
        currentWindowLayer = 0;
    }

    private void InputGetBack()
    {
        if(this.gameObject.activeInHierarchy != true) return;
        inputOk = true;
    }

    // for question option select

    private void ContainerLimit()
    {
        currentItemindex = Mathf.Clamp(currentItemindex, 0, GameManagerUI.instance.CurrentContainer-1);
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
                }
                GameManagerUI.instance.Visualize_Tab_Obtain(false);
            }

            bulletItem = null;
            keyItem = null;
        }else
        {
            if(itemUI.playerInventory.items[itemUI.currentindex] == null) return;
            
            if(currentWindowLayer == 0)
            {
                currentWindowLayer++;
            }
            else if(currentWindowLayer == 1)
            {
                if(itemUI.itemContainers[itemUI.currentindex].selectIndex == 0) // use
                {

                }else if(itemUI.itemContainers[itemUI.currentindex].selectIndex == 1) // combine
                {

                }else if(itemUI.itemContainers[itemUI.currentindex].selectIndex == 2) // discard
                {
                    discardItemEvent.Invoke(itemUI.currentindex);
                    currentWindowLayer--;
                }
            }

            
        }
    }
    public void BackPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        currentWindowLayer--;
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

        }else  if(currentWindowLayer == 2)
        {
            
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
            
        }
    }
    // for question option select

    ////////////// override start /////////////////////////////

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowing && flag) return;

        Tab_Menu_ChangeInput_PauseGame(flag);

        interactiveMessageUI.image.gameObject.SetActive(false);
        interactiveMessageUI.gameObject.GetComponent<Image>().color = new Color(100f / 255f, 150f/ 255f, 200f/ 255f, 220f/ 255f);
        

        isInteractive = true;
        isShowing = flag;
        background.gameObject.SetActive(flag);
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(false);
        equippedUI.gameObject.SetActive(false);
        itemObtainYesNoPanelUI.gameObject.SetActive(false);

    }

    public void Visualize_Tab_Interactive(bool flag , InteractiveDialog dialog)
    {
        Visualize_Tab_Interactive(flag);
        
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
        equippedUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(false);
        itemObtainYesNoPanelUI.gameObject.SetActive(false);
    }

    ////////////////////override start//////////////////////////
    public void Visualize_Tab_Obtain(bool flag)
    {
        if(isShowing && flag) return;

        Tab_Menu_ChangeInput_PauseGame(flag);

        interactiveMessageUI.image.gameObject.SetActive(true);
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
        equippedUI.gameObject.SetActive(false);
        
    }

    public void Visualize_Tab_Obtain(bool flag , bulletItem item)
    {
        Visualize_Tab_Obtain(flag);

        bulletItem = item;

        interactiveMessageUI.SetInteractiveName(item.information.ItemName);
        interactiveMessageUI.SetInteractiveSituation(item.information.ItemDescription[0]);
        interactiveMessageUI.SetItemImage(item.information.ItemImage);
    }

    public void Visualize_Tab_Obtain(bool flag , KeyItem item)
    {
        Visualize_Tab_Obtain(flag);
        keyItem = item;

        interactiveMessageUI.SetInteractiveName(item.information.ItemName);
        interactiveMessageUI.SetInteractiveSituation(item.information.ItemDescription[0]);
        interactiveMessageUI.SetItemImage(item.information.ItemImage);
        
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
