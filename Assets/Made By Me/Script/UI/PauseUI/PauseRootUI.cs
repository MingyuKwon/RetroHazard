using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PauseRootUI : MonoBehaviour
{
    private Player player;
    UI ui;
    public PauseMainUI pauseMainUI;
    public SaveSlotUI saveSlotUI;

    public int CurrentWindowLayer = 0;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

        ui = GetComponentInParent<UI>();
        pauseMainUI = GetComponentInChildren<PauseMainUI>();
        saveSlotUI = GetComponentInChildren<SaveSlotUI>();

        player.AddInputEventDelegate(LeftClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "MouseLeftButton");
        player.AddInputEventDelegate(RightClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "MouseRightButton");

    }
    private void OnEnable() {
        GameMangerInput.instance.changePlayerInputRule(2);
        ui.MouseCursor(true);
        GameManager.instance.SetPauseGame(true);
        CurrentWindowLayer = 0;
    }

    private void OnDisable() { // have an error when game is closed ny force while pause Ui is opened
        GameMangerInput.instance.changePlayerInputRule(0);
        ui.MouseCursor(false);
        GameManager.instance.SetPauseGame(false);
        CurrentWindowLayer = 0;
    }

    private void LeftClicked(InputActionEventData data)
    {

    }
    private void RightClicked(InputActionEventData data)
    {
        CurrentWindowLayer--;
        if(CurrentWindowLayer < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
