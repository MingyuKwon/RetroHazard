using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class TabUI : MonoBehaviour
{
    public bool isShowing = false;
    public bool isInteractive = false;
    public bool isOpenedItem = false;

    private Player player;

    

    public int currentWindowLayer = 0;
    public int currentItemindex = 0;
    public bool yesNoChoice = true;

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

        itemUI = GetComponentInChildren<ItemUI>();
        itemExplainUI = GetComponentInChildren<ItemExplainUI>();
        currentGoalUI = GetComponentInChildren<CurrentGoalUI>();
        interactiveMessageUI = GetComponentInChildren<InteractiveMessageUI>();
        equippedUI = GetComponentInChildren<EquippedUI>();
        itemObtainYesNoPanelUI = GetComponentInChildren<ItemObtainYesNoPanelUI>();
    }

    // for question option select

    private void ContainerLimit()
    {
        currentItemindex = Mathf.Clamp(currentItemindex, 0, GameManagerUI.instance.CurrentContainer-1);
    }
    public void EnterPressed(InputActionEventData data)
    {
        if(isOpenedItem)
        {
            if(yesNoChoice)
            {
                if(bulletItem != null)
                {
                    bulletItem.ObtainBulletItem();
                }else if(keyItem != null)
                {
                    keyItem.EventInvokeOverride();
                }
                
                GameManagerUI.instance.Visualize_Tab_Obtain(false);
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
        }
    }
    public void BackPressed(InputActionEventData data)
    {
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
        if(isOpenedItem) return;
        currentItemindex -= 4;
        ContainerLimit();
    }
    public void DownPressed(InputActionEventData data)
    {
        if(isOpenedItem) return;
        currentItemindex += 4;
        ContainerLimit();
    }
    public void RightPressed(InputActionEventData data)
    {
        if(isOpenedItem)
        {
            yesNoChoice = false;
            return;
        }
        currentItemindex++;
        ContainerLimit();
        
    }
    public void LeftPressed(InputActionEventData data)
    {
        if(isOpenedItem)
        {
            yesNoChoice = true;
            return;
        }
        currentItemindex--;
        ContainerLimit();
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

        GameManager.instance.SetPauseGame(flag);
    }
}
