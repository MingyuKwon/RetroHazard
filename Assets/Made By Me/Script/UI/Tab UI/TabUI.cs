using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;

public class TabUI : MonoBehaviour
{
    public bool isShowing = false;
    public bool isInteractive = false;

    private Player player;

    int currentWindowLayer = 0;

    ItemUI itemUI;
    ItemExplainUI itemExplainUI;
    CurrentGoalUI currentGoalUI;
    InteractiveMessageUI interactiveMessageUI;
    EquippedUI equippedUI;

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
    }

    // for question option select
    public void EnterPressed(InputActionEventData data)
    {

    }
    public void BackPressed(InputActionEventData data)
    {
        currentWindowLayer--;
        if(currentWindowLayer < 0)
        {
            currentWindowLayer = 0;
            if(isInteractive) 
            {
                GameManagerUI.instance.Visualize_Tab_Interactive(false);
            }else
            {
                GameManagerUI.instance.Visualize_Tab_Menu(false);
            }
            
        }

    }
    public void UpPressed(InputActionEventData data)
    {

    }
    public void DownPressed(InputActionEventData data)
    {

    }
    public void RightPressed(InputActionEventData data)
    {
        
    }
    public void LeftPressed(InputActionEventData data)
    {

    }
    // for question option select

    public void Visualize_Tab_Interactive(bool flag)
    {
        if(isShowing && flag) return;

        Tab_Menu_ChangeInput_PauseGame(flag);

        isInteractive = true;
        isShowing = flag;
        itemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
        interactiveMessageUI.gameObject.SetActive(flag);
        currentGoalUI.gameObject.SetActive(false);
        equippedUI.gameObject.SetActive(false);

    }

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
    }

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
