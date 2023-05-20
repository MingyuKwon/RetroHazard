using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;

public class AlertUI : MonoBehaviour
{
    private Player player;
    Text[] texts; // 0 : dialog, 1 : yes, 2 : no 

    public int previousInputRule;

    public static AlertUI instance;

    CallBackInterface CallbackScript;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(LeftClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "MouseLeftButton");
        player.AddInputEventDelegate(RightClicked, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "AlertRightClick");

        texts = GetComponentsInChildren<Text>();

        this.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(LeftClicked);
        player.RemoveInputEventDelegate(RightClicked);
    }

    private void OnEnable() {
        previousInputRule = GameMangerInput.instance.currentInputRule;
        GameMangerInput.instance.changePlayerInputRule(3);
    }

    private void OnDisable() {
        GameMangerInput.instance.changePlayerInputRule(previousInputRule);
        CallbackScript = null;
    }



    public void ShowAlert(string dialog, CallBackInterface CallbackScript , string Yes = "yes", string No = "No")
    {   
        this.gameObject.SetActive(true);
        texts[0].text =  dialog;
        texts[1].text =  Yes;
        texts[2].text =  No;

        this.CallbackScript = CallbackScript;
    }

    public void CloseAlert()
    {
        this.gameObject.SetActive(false);
    }

    public void ActiveCallback()
    {
        CallbackScript.CallBack();
        CloseAlert();
    }

    private void LeftClicked(InputActionEventData data)
    {

    }


    private void RightClicked(InputActionEventData data)
    {
        CloseAlert();
    }
}
