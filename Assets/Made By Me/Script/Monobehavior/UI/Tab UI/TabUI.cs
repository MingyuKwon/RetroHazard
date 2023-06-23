using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class TabUI : MonoBehaviour
{
    public bool inputOk{
        get{
            return tabUILogic.inputOk;
        }

        set{
            tabUILogic.inputOk = value;
        }
    }

    public bool isShowing{
        get{
            return tabUILogic.isShowing;
        }

        set{
            tabUILogic.isShowing = value;
        }
    }

    public bool isInteractive{
        get{
            return tabUILogic.isInteractive;
        }

        set{
            tabUILogic.isInteractive = value;
        }
    }
    public bool isOpenedItem{
        get{
            return tabUILogic.isOpenedItem;
        }

        set{
            tabUILogic.isOpenedItem = value;
        }
    }

    public bool isUseKeyItem{
        get{
            return tabUILogic.isUseKeyItem;
        }

        set{
            tabUILogic.isUseKeyItem = value;
        }
    }

    public int[] neededKeyItemCode{
        get{
            return tabUILogic.neededKeyItemCode;
        }

        set{
            tabUILogic.neededKeyItemCode = value;
        }
    }

    public int currentWindowLayer{
        get{
            return tabUILogic.currentWindowLayer;
        }

        set{
            tabUILogic.currentWindowLayer = value;
        }
    }

    public int currentItemindex{
        get{
            return tabUILogic.currentItemindex;
        }

        set{
            tabUILogic.currentItemindex = value;
        }
    }

    public int previousItemindex{
        get{
            return tabUILogic.previousItemindex;
        }

        set{
            tabUILogic.previousItemindex = value;
        }
    }

    public bool yesNoChoice{
        get{
            return tabUILogic.yesNoChoice;
        }

        set{
            tabUILogic.yesNoChoice = value;
        }
    }

    public ItemInformation combineStartItem{
        get{
            return tabUILogic.combineStartItem;
        }
        set{
            tabUILogic.combineStartItem = value;
        }
    }

    private TabUILogic tabUILogic;

    public ItemUI itemUI;
    public ItemExplainUI itemExplainUI;
    public CurrentGoalUI currentGoalUI;
    public InteractiveMessageUI interactiveMessageUI;
    public ItemObtainYesNoPanelUI itemObtainYesNoPanelUI;


    
    public int combineStartItemIndex{
        get{
            return tabUILogic.combineStartItemIndex;
        }
        set{
            tabUILogic.combineStartItemIndex = value;
        }
    }

    public int discardTargetItemIndex{
        get{
            return tabUILogic.discardTargetItemIndex;
        }
        set{
            tabUILogic.discardTargetItemIndex = value;
        }
    }

    private void Awake() {
        itemUI = GetComponentInChildren<ItemUI>();
        itemExplainUI = GetComponentInChildren<ItemExplainUI>();
        currentGoalUI = GetComponentInChildren<CurrentGoalUI>();
        interactiveMessageUI = GetComponentInChildren<InteractiveMessageUI>();
        itemObtainYesNoPanelUI = GetComponentInChildren<ItemObtainYesNoPanelUI>();

        tabUILogic = new TabUILogic(transform.GetChild(0).GetComponent<Image>(), this);
    }

    private void OnEnable() {
        tabUILogic.OnEnable();
    }

    private void OnDisable() {
        tabUILogic.OnDisable();
    }

    public void UpdateTabUI()
    {
        itemUI.UpdateInventoryUI();
        currentGoalUI.ChangeGoalText(PlayerGoalCollection.PlayerGoals[PlayerGoalCollection.currentGoalIndex]);
    }
    // for question option select

    public void CallBack()
    {
        GameManager.EventManager.Invoke_discardItemEvent(discardTargetItemIndex);
        currentWindowLayer--;
        discardTargetItemIndex = -1;
    }
    // for question option select

    ////////////// override start /////////////////////////////

    public void Visualize_Tab_Interactive(bool flag)
    {
        tabUILogic.Visualize_Tab_Interactive(flag);

    }

    public void Visualize_Tab_Interactive(bool flag , InteractiveDialog dialog)
    {
        tabUILogic.Visualize_Tab_Interactive(flag, dialog);

    }

    ////////////// override end /////////////////////////////

    public void Visualize_Tab_Menu(bool flag)
    {
        tabUILogic.Visualize_Tab_Menu(flag);
    }

    ////////////////////override start//////////////////////////
    public void Visualize_Tab_Obtain(bool flag)
    {
        tabUILogic.Visualize_Tab_Obtain(flag);
    }

    public void Visualize_Tab_Obtain(bool flag , bulletItem item)
    {
        tabUILogic.Visualize_Tab_Obtain(flag, item);
    }

    public void Visualize_Tab_Obtain(bool flag , PotionItem item)
    {
        tabUILogic.Visualize_Tab_Obtain(flag, item);
    }

    public void Visualize_Tab_Obtain(bool flag , KeyItem item)
    {
        tabUILogic.Visualize_Tab_Obtain(flag,item);
    }
    
    ////////////////////override end//////////////////////////

}
