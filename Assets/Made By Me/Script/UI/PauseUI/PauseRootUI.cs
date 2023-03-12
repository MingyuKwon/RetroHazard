using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PauseRootUI : MonoBehaviour
{
    public static event Action windowLayer_Change_Event;
    private Player player;
    UI ui;
    public PauseMainUI pauseMainUI;
    public SaveSlotUI saveSlotUI;
    public OptionUI optionUI;

    public int CurrentWindowLayer = 0;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

        ui = GetComponentInParent<UI>();
        pauseMainUI = GetComponentInChildren<PauseMainUI>();
        saveSlotUI = GetComponentInChildren<SaveSlotUI>();
        optionUI = GetComponentInChildren<OptionUI>();

        player.AddInputEventDelegate(LeftClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "MouseLeftButton");
        player.AddInputEventDelegate(RightClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "MouseRightButton");

    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(LeftClicked);
        player.RemoveInputEventDelegate(RightClicked);
    }

    private void OnEnable() {
        GameMangerInput.instance.changePlayerInputRule(2);
        ui.MouseCursor(true);
        GameManager.instance.SetPauseGame(true);
        CurrentWindowLayer = 0;

        windowLayer_Change_Event += windowLayer_Check;

        windowLayer_Change_Event.Invoke();
    }

    private void OnDisable() { // have an error when game is closed ny force while pause Ui is opened
        GameMangerInput.instance.changePlayerInputRule(0);
        ui.MouseCursor(false);
        GameManager.instance.SetPauseGame(false);
        CurrentWindowLayer = 0;

        windowLayer_Change_Event -= windowLayer_Check;
    }

    private void LeftClicked(InputActionEventData data)
    {

    }
    private void RightClicked(InputActionEventData data)
    {
        if(CurrentWindowLayer == 0 )
        {
            CurrentWindowLayer--;
        }else
        {
            CurrentWindowLayer = 0;
        }
        
        windowLayer_Change_Event.Invoke();
        
    }

    public void windowLayer_Change_Invoke()
    {
        windowLayer_Change_Event.Invoke();
    }

    private void windowLayer_Check()
    {
        if(CurrentWindowLayer < 0)
        {
            this.gameObject.SetActive(false);
        }

        if(CurrentWindowLayer == 0)
        {
            pauseMainUI.gameObject.SetActive(true);
            saveSlotUI.gameObject.SetActive(false);
            optionUI.gameObject.SetActive(false);
        }

        if(CurrentWindowLayer == 1)
        {
           pauseMainUI.gameObject.SetActive(false);
           saveSlotUI.gameObject.SetActive(true);
           optionUI.gameObject.SetActive(false);
        }

        if(CurrentWindowLayer == 2)
        {
           pauseMainUI.gameObject.SetActive(false);
           saveSlotUI.gameObject.SetActive(false);
           optionUI.gameObject.SetActive(true);
        }
    }
}
